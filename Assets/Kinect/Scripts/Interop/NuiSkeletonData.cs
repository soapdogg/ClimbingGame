using System.Runtime.InteropServices;
using UnityEngine;


public enum NuiSkeletonPositionTrackingState {}

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

