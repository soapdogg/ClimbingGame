using UnityEngine;

public interface IKinectSensor
{
    /// <summary>
    /// how high (in meters) off the ground is the sensor
    /// </summary>
    float SensorHeight { get; set; }

    /// <summary>
    /// where (relative to the ground directly under the sensor) should the kinect register as 0,0,0
    /// </summary>
    Vector3 KinectCenter { get; set; }
        
    /// <summary>
    /// what point (relative to KinectCenter) should the sensor look at
    /// </summary>
    Vector4 LookAt { get; set; }

    bool PollSkeleton();

    NuiSkeletonFrame GetSkeleton();
}
