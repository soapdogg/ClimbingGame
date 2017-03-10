using System.Runtime.InteropServices;
using UnityEngine;

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

