using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Kinect
{
	public struct NuiStatusProc
	{
		
	}
	
	public interface IKinectInterface
    {
		float GetSensorHeight();
		Vector3 GetKinectCenter();
		Vector4 GetLookAt();
		bool PollSkeleton();
		NuiSkeletonFrame GetSkeleton();
	}

    /// <summary>
    ///Structs and constants for interfacing c# with the c++ kinect dll 
    /// </summary>
    [Flags]
    public enum NuiInitializeFlags : uint
    {
        UsesDepthAndPlayerIndex = 0x00000001,
        UsesColor = 0x00000002,
        UsesSkeleton = 0x00000008,
        UsesDepth = 0x00000020
    }

    public enum NuiSkeletonPositionTrackingState
    {
        NotTracked = 0,
        Inferred,
        Tracked
    }

    public enum NuiSkeletonTrackingState
    {
        NotTracked = 0,
        PositionOnly,
        SkeletonTracked
    }
		
    [StructLayout(LayoutKind.Sequential)]
    public struct NuiSkeletonData
    {
        public NuiSkeletonTrackingState eTrackingState;
        public uint dwTrackingID;
        public uint dwEnrollmentIndex_NotUsed;
        public uint dwUserIndex;
        public Vector4 Position;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.Struct)]
        public Vector4[] SkeletonPositions;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.Struct)]
        public NuiSkeletonPositionTrackingState[] eSkeletonPositionTrackingState;
        public uint dwQualityFlags;
    }
	
    public struct NuiSkeletonFrame
    {
        public long liTimeStamp;
        public uint dwFrameNumber;
        public uint dwFlags;
        public Vector4 vFloorClipPlane;
        public Vector4 vNormalToGravity;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.Struct)]
        public NuiSkeletonData[] SkeletonData;
    }
	
	public struct NuiTransformSmoothParameters
	{
		public float fSmoothing;
		public float fCorrection;
		public float fPrediction;
		public float fJitterRadius;
		public float fMaxDeviationRadius;
	}
	
	public enum NuiSkeletonFlags
	{
		SUPPRESS_NO_FRAME_DATA = 0x00000001,
		TITLE_SETS_TRACKED_SKELETONS = 0x00000002,
		ENABLE_SEATED_SUPPORT = 0x00000004,
		ENABLE_IN_NEAR_RANGE = 0x00000008
	}
		
	public class NativeMethods
	{
		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiSetDeviceStatusCallback")]
	    public static extern void NuiSetDeviceStatusCallback(NuiStatusProc callback, IntPtr pUserData);
		
	    [DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiInitialize")]
	    public static extern int NuiInitialize(NuiInitializeFlags dwFlags);
		
		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiShutdown")]
	    public static extern void NuiShutdown();
		
		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiCameraElevationSetAngle")]
		public static extern int NuiCameraSetAngle(long angle);
		

		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiSkeletonTrackingEnable")]
	    public static extern int NuiSkeletonTrackingEnable(IntPtr hNextFrameEvent, NuiSkeletonFlags dwFlags);
		
	    [DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiSkeletonGetNextFrame")]
	    public static extern int NuiSkeletonGetNextFrame(uint dwMillisecondsToWait, ref NuiSkeletonFrame pSkeletonFrame);

		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiTransformSmooth")]
	    public static extern int NuiTransformSmooth(ref NuiSkeletonFrame pSkeletonFrame,ref NuiTransformSmoothParameters pSmoothingParams);
	}
	
}
