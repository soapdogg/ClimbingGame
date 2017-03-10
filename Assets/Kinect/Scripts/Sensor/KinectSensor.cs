using System;
using UnityEngine;

public class KinectSensor : MonoBehaviour, IKinectSensor
{
    public static IKinectSensor Instance { get; private set; }
	
	public float SensorHeight { get; set; }

	public Vector3 KinectCenter { get; set; }

	public Vector4 LookAt { get; set; }
	
	public NuiSkeletonFlags skeltonTrackingMode;
	

	private bool updatedSkeleton;
	private bool newSkeleton;
	private NuiSkeletonFrame skeletonFrame = new NuiSkeletonFrame { SkeletonData = new NuiSkeletonData[1] };	
	
	
	void Awake()
	{
	    if (Instance != null) return;
	    try
	    {
	        int hr = NativeMethods.NuiInitialize(NuiInitializeFlags.UsesSkeleton);
	        if (hr != 0) throw new Exception("NuiInitialize Failed.");     

	        hr = NativeMethods.NuiSkeletonTrackingEnable(IntPtr.Zero, skeltonTrackingMode);
	        if (hr != 0) throw new Exception("Cannot initialize Skeleton Data.");
	        
	        SetAngle();

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

	public bool PollSkeleton()
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
				NativeMethods.NuiTransformSmoothWrapper(ref skeletonFrame);
			}
			return newSkeleton;
		}
		catch (DllNotFoundException e)
        {
			return false;
		}
	}
	
	public NuiSkeletonFrame GetSkeleton()
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

    private void SetAngle()
    {
        double theta = Mathf.Atan((LookAt.y + KinectCenter.y - SensorHeight) / (LookAt.z + KinectCenter.z));
        long kinectAngle = (long)(theta * (180 / Mathf.PI));
        NativeMethods.NuiCameraSetAngle(kinectAngle);
    }
}
