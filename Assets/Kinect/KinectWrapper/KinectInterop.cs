using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
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
		
		NuiSkeletonBoneOrientation[] GetBoneOrientations(NuiSkeletonData skeletonData);
	}
	
	public static class Constants
	{
		public static int NuiSkeletonCount = 6;
    	public static int NuiSkeletonMaxTracked = 2;
    	public static int NuiSkeletonInvalidTrackingID = 0;
		
		public static float NuiDepthHorizontalFOV = 58.5f;
		public static float NuiDepthVerticalFOV = 45.6f;
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

    public enum NuiSkeletonPositionIndex : int
    {
        HipCenter = 0,
        Spine = 1,
        ShoulderCenter = 2,
        Head = 3,
        ShoulderLeft = 4,
        ElbowLeft = 5,
        WristLeft = 6,
        HandLeft = 7,
        ShoulderRight = 8,
        ElbowRight = 9,
        WristRight = 10,
        HandRight = 11,
        HipLeft = 12,
        KneeLeft = 13,
        AnkleLeft = 14,
        FootLeft = 15,
        HipRight = 16,
        KneeRight = 17,
        AnkleRight = 18,
        FootRight = 19,
        Count = 20
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
	
	public enum NuiImageType
	{
		DepthAndPlayerIndex = 0,	// USHORT
		Color,						// RGB32 data
		ColorYUV,					// YUY2 stream from camera h/w, but converted to RGB32 before user getting it.
		ColorRawYUV,				// YUY2 stream from camera h/w.
		Depth						// USHORT
	}
	
	public enum NuiImageResolution
	{
		resolutionInvalid = -1,
		resolution80x60 = 0,
		resolution320x240,
		resolution640x480,
		resolution1280x1024                        // for hires color only
	}


	public enum NuiImageStreamFlags
	{
		None = 0x00000000,
		SupressNoFrameData = 0x0001000,
		EnableNearMode = 0x00020000,
		TooFarIsNonZero = 0x0004000
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
	
	public struct NuiSkeletonBoneOrientation
	{
		public NuiSkeletonPositionIndex endJoint;
		public NuiSkeletonPositionIndex startJoint;
		public NuiSkeletonBoneRotation hierarchicalRotation; // local orientation
		public NuiSkeletonBoneRotation absoluteRotation; // world orientation
	}
	
	public struct NuiSkeletonBoneRotation
    {
		public SerialMatrix4 rotationMatrix;
		public SerialVec4 rotationQuaternion;
	}
	

	[Serializable]
	public struct SerialVec4
    {
		float x,y,z,w;
		
		public SerialVec4(Vector4 vec)
        {
			x = vec.x;
			y = vec.y;
			z = vec.z;
			w = vec.w;
		}
		
		public Vector4 Deserialize()
        {
			return new Vector4(x,y,z,w);
		}
		
		public Quaternion GetQuaternion()
        {
			return new Quaternion(x,y,z,w);
		}
	}
	
	[Serializable]
	public struct SerialMatrix4
    {
		float m11;
		float m12;
		float m13;
		float m14;
		float m21;
		float m22;
		float m23;
		float m24;
		float m31;
		float m32;
		float m33;
		float m34;
		float m41;
		float m42;
		float m43;
		float m44;
		
		public SerialMatrix4(Matrix4x4 mat)
        {
			m11 = mat.m00;
			m12 = mat.m01;
			m13 = mat.m02;
			m14 = mat.m03;
			m21 = mat.m10;
			m22 = mat.m11;
			m23 = mat.m12;
			m24 = mat.m13;
			m31 = mat.m20;
			m32 = mat.m21;
			m33 = mat.m22;
			m34 = mat.m23;
			m41 = mat.m30;
			m42 = mat.m31;
		    m43 = mat.m32;
			m44 = mat.m33;
		}
		
		public Matrix4x4 Deserialize()
        {
			Matrix4x4 mat = new Matrix4x4();
			
			mat.m00 = m11;
			mat.m01 = m12;
			mat.m02 = m13;
			mat.m03 = m14;
			mat.m10 = m21;
			mat.m11 = m22;
			mat.m12 = m23;
			mat.m13 = m24;
			mat.m20 = m31;
			mat.m21 = m32;
			mat.m22 = m33;
			mat.m23 = m34;
			mat.m30 = m41;
			mat.m31 = m42;
			mat.m32 = m43;
			mat.m33 = m44;
					
			return mat;
		}
	}
	

	
	[Serializable]
	public struct SerialSkeletonData
    {
		public NuiSkeletonTrackingState eTrackingState;
        public uint dwTrackingID;
        public uint dwEnrollmentIndex_NotUsed;
        public uint dwUserIndex;
        public SerialVec4 Position;
        public SerialVec4[] SkeletonPositions;
        public NuiSkeletonPositionTrackingState[] eSkeletonPositionTrackingState;
        public uint dwQualityFlags;
		
		public SerialSkeletonData (NuiSkeletonData nui)
        {
			this.eTrackingState = nui.eTrackingState;
	        this.dwTrackingID = nui.dwTrackingID;
	        this.dwEnrollmentIndex_NotUsed = nui.dwEnrollmentIndex_NotUsed;
	        this.dwUserIndex = nui.dwUserIndex;
	        this.Position = new SerialVec4(nui.Position);
	        this.SkeletonPositions = new SerialVec4[20];
			for(int ii = 0; ii < 20; ii++){
				this.SkeletonPositions[ii] = new SerialVec4(nui.SkeletonPositions[ii]);
			}
	        this.eSkeletonPositionTrackingState = nui.eSkeletonPositionTrackingState;
	        this.dwQualityFlags = nui.dwQualityFlags;
		}
		
		public NuiSkeletonData deserialize() {
			NuiSkeletonData nui = new NuiSkeletonData();
			nui.eTrackingState = this.eTrackingState;
	        nui.dwTrackingID = this.dwTrackingID;
	        nui.dwEnrollmentIndex_NotUsed = this.dwEnrollmentIndex_NotUsed;
	        nui.dwUserIndex = this.dwUserIndex;
	        nui.Position = this.Position.Deserialize();
	        nui.SkeletonPositions = new Vector4[20];
			for(int ii = 0; ii < 20; ii++){
				nui.SkeletonPositions[ii] = this.SkeletonPositions[ii].Deserialize();
			}
	        nui.eSkeletonPositionTrackingState = this.eSkeletonPositionTrackingState;
	        nui.dwQualityFlags = this.dwQualityFlags;
			return nui;
		}
	}
	
	[Serializable]
	public struct SerialSkeletonFrame
	{
		public long liTimeStamp;
        public uint dwFrameNumber;
        public uint dwFlags;
        public SerialVec4 vFloorClipPlane;
        public SerialVec4 vNormalToGravity;
        public SerialSkeletonData[] SkeletonData;
		
		public SerialSkeletonFrame (NuiSkeletonFrame nui)
        {
			this.liTimeStamp = nui.liTimeStamp;
			this.dwFrameNumber = nui.dwFrameNumber;
			this.dwFlags = nui.dwFlags;
			this.vFloorClipPlane = new SerialVec4(nui.vFloorClipPlane);
			this.vNormalToGravity = new SerialVec4(nui.vNormalToGravity);
			this.SkeletonData = new SerialSkeletonData[6];
			for(int ii = 0; ii < 6; ii++){
				this.SkeletonData[ii] = new SerialSkeletonData(nui.SkeletonData[ii]);
			}
		}
		
		public NuiSkeletonFrame Deserialize()
         {
			NuiSkeletonFrame nui = new NuiSkeletonFrame();
			nui.liTimeStamp = this.liTimeStamp;
			nui.dwFrameNumber = this.dwFrameNumber;
			nui.dwFlags = this.dwFlags;
			nui.vFloorClipPlane = this.vFloorClipPlane.Deserialize();
			nui.vNormalToGravity = this.vNormalToGravity.Deserialize();
			nui.SkeletonData = new NuiSkeletonData[6];
			for(int ii = 0; ii < 6; ii++){
				nui.SkeletonData[ii] = this.SkeletonData[ii].deserialize();
			}
			return nui;
		}
	}
	
	public struct NuiImageViewArea
	{

	}
	
	// Reference: http://msdn.microsoft.com/en-us/library/nuiimagecamera.nui_image_frame.aspx
	[StructLayout(LayoutKind.Sequential)]
	public struct NuiImageFrame
	{
		public long liTimeStamp;
		public uint dwFrameNumber;
		public NuiImageType eImageType;
		public NuiImageResolution eResolution;
		public IntPtr pFrameTexture;
		public uint dwFrameFlags_NotUsed;
		public NuiImageViewArea ViewArea_NotUsed;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorCust
	{
		public byte b;
		public byte g;
		public byte r;
		public byte a;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorBuffer
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 640 * 480, ArraySubType = UnmanagedType.Struct)]
		public ColorCust[] pixels;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct DepthBuffer
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 320 * 240, ArraySubType = UnmanagedType.I2)]
		public short[] pixels;
	}

	[StructLayoutAttribute(LayoutKind.Sequential)]
	public struct NuiLockedRect
	{
		public int pitch;
		public int size;
		public IntPtr pBits; 
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct NuiSurfaceDesc
    {
		uint width;
		uint height;
	}
	
	// to marshal the data from NuiImageFrame to this struct
	// reference: http://msdn.microsoft.com/en-us/library/nuisensor.inuiframetexture.aspx
	[Guid("13ea17f5-ff2e-4670-9ee5-1297a6e880d1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport()]
	public interface INuiFrameTexture
	{
		[MethodImpl (MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		[PreserveSig]
		int LockRect(uint Level,ref NuiLockedRect pLockedRect,IntPtr pRect, uint Flags);
		[MethodImpl (MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		[PreserveSig]
		int GetLevelDesc(uint Level, ref NuiSurfaceDesc pDesc);
		[MethodImpl (MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		[PreserveSig]
		int UnlockRect(uint Level);
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
		
		/*
		 * kinect skeleton functions
		 */
		
		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiSkeletonTrackingEnable")]
	    public static extern int NuiSkeletonTrackingEnable(IntPtr hNextFrameEvent, NuiSkeletonFlags dwFlags);
		
	    [DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiSkeletonGetNextFrame")]
	    public static extern int NuiSkeletonGetNextFrame(uint dwMillisecondsToWait, ref NuiSkeletonFrame pSkeletonFrame);

		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiTransformSmooth")]
	    public static extern int NuiTransformSmooth(ref NuiSkeletonFrame pSkeletonFrame,ref NuiTransformSmoothParameters pSmoothingParams);
		
		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiSkeletonCalculateBoneOrientations")]
	    public static extern uint NuiSkeletonCalculateBoneOrientations([In] ref NuiSkeletonData pSkeletonData, [Out] NuiSkeletonBoneOrientation[] pBoneOrientations);
		
		
		/*
		 * kinect video functions
		 */
		
		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiImageStreamOpen")]
	    public static extern int NuiImageStreamOpen(NuiImageType eImageType, NuiImageResolution eResolution, uint dwImageFrameFlags_NotUsed, uint dwFrameLimit, IntPtr hNextFrameEvent, ref IntPtr phStreamHandle);
		
		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiImageStreamGetNextFrame")]
	    public static extern int NuiImageStreamGetNextFrame(IntPtr phStreamHandle, uint dwMillisecondsToWait, ref IntPtr ppcImageFrame);
		
		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiImageStreamReleaseFrame")]
	    public static extern int NuiImageStreamReleaseFrame(IntPtr phStreamHandle, IntPtr ppcImageFrame);
		
		[DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiImageStreamSetImageFrameFlags")]
		public static extern int NuiImageStreamSetImageFrameFlags (IntPtr phStreamHandle, NuiImageStreamFlags dvImageFrameFlags);
	}
	
}
