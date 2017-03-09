using System;
using UnityEngine;
using Kinect;

public class KinectSensor : MonoBehaviour, IKinectInterface
{
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
    private const float SMOOTHING = 0.5f;
    private const float CORRECTION=0.5f;
	private const float PREDICTION=0.5f;
	private const float JITTER_RADIUS=0.05f;
	private const float MAX_DEVIATION_RADIUS=0.04f;
	
	public NuiSkeletonFlags skeltonTrackingMode;
	
	/// <summary>
	///variables used for updating and accessing depth data 
	/// </summary>
	private bool updatedSkeleton;
	private bool newSkeleton;
	private NuiSkeletonFrame skeletonFrame = new NuiSkeletonFrame { SkeletonData = new NuiSkeletonData[1] };
	
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
	        int hr =
	            NativeMethods.NuiInitialize(NuiInitializeFlags.UsesSkeleton);
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
				smoothParameters.fSmoothing = SMOOTHING;
				smoothParameters.fCorrection = CORRECTION;
				smoothParameters.fJitterRadius = JITTER_RADIUS;
				smoothParameters.fMaxDeviationRadius = MAX_DEVIATION_RADIUS;
				smoothParameters.fPrediction = PREDICTION;
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
