using UnityEngine;

public class SkeletonWrapper : MonoBehaviour
{
	public static SkeletonWrapper singleton { get; private set; }
	private IKinectSensor kinect;

    private bool updatedSkeleton;
	private bool newSkeleton;
	private NuiSkeletonTrackingState player;

	[HideInInspector]
	public Vector3[] bonePos;

    private const int BONE_COUNT = 20;	
	private Matrix4x4 kinectToWorld;
	public Matrix4x4 flipMatrix;
	
	void Start ()
	{
	    singleton = this;
	    kinect = KinectSensor.Instance;
		player = new NuiSkeletonTrackingState();
		bonePos = new Vector3[BONE_COUNT];
		
		//create the transform matrix that converts from kinect-space to world-space
		Matrix4x4 trans = new Matrix4x4();
		trans.SetTRS( new Vector3(-kinect.KinectCenter.x,
		                          kinect.SensorHeight-kinect.KinectCenter.y,
		                          -kinect.KinectCenter.z),
		             Quaternion.identity, Vector3.one );
		Matrix4x4 rot = new Matrix4x4();
		Quaternion quat = new Quaternion();
		double theta = Mathf.Atan((kinect.LookAt.y+kinect.KinectCenter.y-kinect.SensorHeight) / (kinect.LookAt.z + kinect.KinectCenter.z));
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
		player = kinect.GetSkeleton().SkeletonData[0].eTrackingState;
	    if (player != NuiSkeletonTrackingState.SkeletonTracked) return;
	    for (int bone = 0; bone < BONE_COUNT; bone++)
	    {
	        bonePos[bone] = kinectToWorld.MultiplyPoint3x4(kinect.GetSkeleton().SkeletonData[0].SkeletonPositions[bone]);
	    }
    }
	
}
