using System;
using System.Runtime.InteropServices;

public class NativeMethods
{
    /// <summary>
    /// Variables used to pass to smoothing function. Values are set to default based on Action in Motion's Research
    /// </summary>
    private const float SMOOTHING = 0.5f;
    private const float CORRECTION = 0.5f;
    private const float PREDICTION = 0.5f;
    private const float JITTER_RADIUS = 0.05f;
    private const float MAX_DEVIATION_RADIUS = 0.04f;

    private static  NuiTransformSmoothParameters defaultSmoothingParameters = new NuiTransformSmoothParameters
    {
       fSmoothing = SMOOTHING,
       fCorrection = CORRECTION,
       fPrediction = PREDICTION,
       fJitterRadius = JITTER_RADIUS,
       fMaxDeviationRadius = MAX_DEVIATION_RADIUS
    };


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

    public static int NuiTransformSmoothWrapper(ref NuiSkeletonFrame pSkeletonFrame)
    {
        return NuiTransformSmooth(ref pSkeletonFrame, ref defaultSmoothingParameters);
    }

    [DllImport(@"C:\Windows\System32\Kinect10.dll", EntryPoint = "NuiTransformSmooth")]
    public static extern int NuiTransformSmooth(ref NuiSkeletonFrame pSkeletonFrame, ref NuiTransformSmoothParameters pSmoothingParams);
}
