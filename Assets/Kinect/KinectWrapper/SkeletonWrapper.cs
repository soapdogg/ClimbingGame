using UnityEngine;

public class SkeletonWrapper : MonoBehaviour
{
	public static SkeletonWrapper singleton { get; private set; }
	private Kinect.IKinectInterface kinect;

    private bool updatedSkeleton;
	private bool newSkeleton;
	
	private Kinect.NuiSkeletonTrackingState players;
	private int trackedPlayers;
	[HideInInspector]
	public Vector3[] bonePos;

    private const int BONE_COUNT = 20;
	
	private Matrix4x4 kinectToWorld;
	public Matrix4x4 flipMatrix;
	
	// Use this for initialization
	void Start ()
	{
	    singleton = this;
	    kinect = KinectSensor.Instance;
		players = new Kinect.NuiSkeletonTrackingState();
		trackedPlayers = -1;
		bonePos = new Vector3[BONE_COUNT];
		
		//create the transform matrix that converts from kinect-space to world-space
		Matrix4x4 trans = new Matrix4x4();
		trans.SetTRS( new Vector3(-kinect.GetKinectCenter().x,
		                          kinect.GetSensorHeight()-kinect.GetKinectCenter().y,
		                          -kinect.GetKinectCenter().z),
		             Quaternion.identity, Vector3.one );
		Matrix4x4 rot = new Matrix4x4();
		Quaternion quat = new Quaternion();
		double theta = Mathf.Atan((kinect.GetLookAt().y+kinect.GetKinectCenter().y-kinect.GetSensorHeight()) / (kinect.GetLookAt().z + kinect.GetKinectCenter().z));
		float kinectAngle = (float)(theta * (180 / Mathf.PI));
		quat.eulerAngles = new Vector3(-kinectAngle, 0, 0);
		rot.SetTRS( Vector3.zero, quat, Vector3.one);

		//final transform matrix offsets the rotation of the kinect, then translates to a new center
		kinectToWorld = flipMatrix*trans*rot;
	}
	
	void LateUpdate ()
    {
		updatedSkeleton = false;
		newSkeleton = false;
	}
	
	public bool PollSkeleton ()
    {
		if (!updatedSkeleton)
		{
			updatedSkeleton = true;
			if (kinect.PollSkeleton())
			{
				newSkeleton = true;
				ProcessSkeleton();
			}
		}
		return newSkeleton;
	}
	
	private void ProcessSkeleton ()
    {
		players = kinect.GetSkeleton().SkeletonData[0].eTrackingState;
		if (players == Kinect.NuiSkeletonTrackingState.SkeletonTracked)
		{
            for (int bone = 0; bone < BONE_COUNT; bone++)
            {
                bonePos[bone] = kinectToWorld.MultiplyPoint3x4(kinect.GetSkeleton().SkeletonData[0].SkeletonPositions[bone]);
            }
        }
	}
	
}
