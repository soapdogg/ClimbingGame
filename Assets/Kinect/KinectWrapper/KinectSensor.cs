using System;
using UnityEngine;
using Kinect;

public class KinectSensor : MonoBehaviour, IKinectInterface
{
	//make KinectSensor a singleton (sort of)
	private static IKinectInterface instance;
    public static IKinectInterface Instance
    {
        get
        {
            if (instance == null)
                throw new Exception("There needs to be an active instance of the KinectSensor component.");
            return instance;
        }
        private set
        { instance = value; }
    }
	
	/// <summary>
	/// how high (in meters) off the ground is the sensor
	/// </summary>
	public float sensorHeight;
	/// <summary>
	/// where (relative to the ground directly under the sensor) should the kinect register as 0,0,0
	/// </summary>
	public Vector3 kinectCenter;
	/// <summary>
	/// what point (relative to kinectCenter) should the sensor look at
	/// </summary>
	public Vector4 lookAt;
	
	/// <summary>
	/// Variables used to pass to smoothing function. Values are set to default based on Action in Motion's Research
	/// </summary>
	private float smoothing =0.5f;	
	private float correction=0.5f;
	private float prediction=0.5f;
	private float jitterRadius=0.05f;
	private float maxDeviationRadius=0.04f;

    public bool enableNearMode = false;
	
	public NuiSkeletonFlags skeltonTrackingMode;
	
	
	/// <summary>
	///variables used for updating and accessing depth data 
	/// </summary>
	private bool updatedSkeleton;
	private bool newSkeleton;
	[HideInInspector]
	private NuiSkeletonFrame skeletonFrame = new NuiSkeletonFrame { SkeletonData = new NuiSkeletonData[6] };
	
	//image stream handles for the kinect
	private IntPtr colorStreamHandle;
	private IntPtr depthStreamHandle;
    private NuiTransformSmoothParameters smoothParameters;
	
	float IKinectInterface.GetSensorHeight()
    {
		return sensorHeight;
	}

	Vector3 IKinectInterface.GetKinectCenter()
    {
		return kinectCenter;
	}

	Vector4 IKinectInterface.GetLookAt()
    {
		return lookAt;
	}
	
	
	void Awake()
	{
	    if (instance != null) return;
	    try
	    {
	        // The MSR Kinect DLL (native code) is going to load into the Unity process and stay resident even between debug runs of the game.  
	        // So our component must be resilient to starting up on a second run when the Kinect DLL is already loaded and
	        // perhaps even left in a running state.  Kinect does not appear to like having NuiInitialize called when it is already initialized as
	        // it messes up the internal state and stops functioning.  It is resilient to having Shutdown called right before initializing even if it
	        // hasn't been initialized yet.  So calling this first puts us in a good state on a first or second run.
	        // However, calling NuiShutdown before starting prevents the image streams from being read, so if you want to use image data
	        // (either depth or RGB), comment this line out.
	        //NuiShutdown();

	        int hr =
	            NativeMethods.NuiInitialize(NuiInitializeFlags.UsesDepthAndPlayerIndex |
	                                        NuiInitializeFlags.UsesSkeleton | NuiInitializeFlags.UsesColor);
	        if (hr != 0)
	        {
	            throw new Exception("NuiInitialize Failed.");
	        }

	        hr = NativeMethods.NuiSkeletonTrackingEnable(IntPtr.Zero, skeltonTrackingMode);
	        if (hr != 0)
	        {
	            throw new Exception("Cannot initialize Skeleton Data.");
	        }

	        double theta = Mathf.Atan((lookAt.y + kinectCenter.y - sensorHeight)/(lookAt.z + kinectCenter.z));
	        long kinectAngle = (long) (theta*(180/Mathf.PI));
	        NativeMethods.NuiCameraSetAngle(kinectAngle);

	        //DontDestroyOnLoad(gameObject);
	        Instance = this;
	        NativeMethods.NuiSetDeviceStatusCallback(new NuiStatusProc(), IntPtr.Zero);
	    }

	    catch (Exception e)
	    {
	        Debug.Log(e.Message);
	    }
	}

    void LateUpdate()
	{
		updatedSkeleton = false;
		newSkeleton = false;
	}

	/// <summary>
	///The first time in each frame that it is called, poll the kinect for updated skeleton data and return
	///true if there is new data. Subsequent calls do nothing and return the same value.
	/// </summary>
	/// <returns>
	/// A <see cref="System.Boolean"/> : is there new data this frame
	/// </returns>
	bool IKinectInterface.PollSkeleton()
	{
		try
		{
			if (!updatedSkeleton)
			{
				updatedSkeleton = true;
				int hr = NativeMethods.NuiSkeletonGetNextFrame(100,ref skeletonFrame);
				if(hr == 0)
				{
					newSkeleton = true;
				}
				smoothParameters.fSmoothing = smoothing;
				smoothParameters.fCorrection = correction;
				smoothParameters.fJitterRadius = jitterRadius;
				smoothParameters.fMaxDeviationRadius = maxDeviationRadius;
				smoothParameters.fPrediction = prediction;
				NativeMethods.NuiTransformSmooth(ref skeletonFrame,ref smoothParameters);
			}
			return newSkeleton;
		}
		catch (DllNotFoundException e)
        {
			return false;
		}
	}
	
	NuiSkeletonFrame IKinectInterface.GetSkeleton()
    {
		return skeletonFrame;
	}
	
	/// <summary>
	/// Get all bones orientation based on the skeleton passed in
	/// </summary>
	/// <returns>
	/// Bone Orientation in struct of NuiSkeletonBoneOrientation, quarternion and matrix
	/// </returns>
	NuiSkeletonBoneOrientation[] IKinectInterface.GetBoneOrientations(NuiSkeletonData skeletonData)
    {
		NuiSkeletonBoneOrientation[] boneOrientations = new NuiSkeletonBoneOrientation[(int)(NuiSkeletonPositionIndex.Count)];
		NativeMethods.NuiSkeletonCalculateBoneOrientations(ref skeletonData, boneOrientations);
		return boneOrientations;
	}
	
	void OnApplicationQuit()
	{
		try
		{
			NativeMethods.NuiShutdown();
		}
		catch(DllNotFoundException e)
		{
			
		}
	}
	
}
