using UnityEngine;

public class KinectPointController : MonoBehaviour
{
	//Assignments for a bitmask to control which bones to look at and which to ignore
	private enum BoneMask
	{
		None = 0x0,
		Hip_Center = 0x1,
		Spine = 0x2,
		Shoulder_Center = 0x4,
		Head = 0x8,
		Shoulder_Left = 0x10,
		Elbow_Left = 0x20,
		Wrist_Left = 0x40,
		Hand_Left = 0x80,
		Shoulder_Right = 0x100,
		Elbow_Right = 0x200,
		Wrist_Right = 0x400,
		Hand_Right = 0x800,
		Hip_Left = 0x1000,
		Knee_Left = 0x2000,
		Ankle_Left = 0x4000,
		Foot_Left = 0x8000,
		Hip_Right = 0x10000,
		Knee_Right = 0x20000,
		Ankle_Right = 0x40000,
		Foot_Right = 0x80000,
		All = 0xFFFFF,
		Torso = 0x10000F, //the leading bit is used to force the ordering in the editor
		Left_Arm = 0x1000F0,
		Right_Arm = 0x100F00,
		Left_Leg = 0x10F000,
		Right_Leg = 0x1F0000,
        Extremities = Hand_Left | Hand_Right | Foot_Left | Foot_Right
	}
	
	public GameObject Hip_Center;
	public GameObject Spine;
	public GameObject Shoulder_Center;
	public GameObject Head;
	public GameObject Shoulder_Left;
	public GameObject Elbow_Left;
	public GameObject Wrist_Left;
	public GameObject Hand_Left;
	public GameObject Shoulder_Right;
	public GameObject Elbow_Right;
	public GameObject Wrist_Right;
	public GameObject Hand_Right;
	public GameObject Hip_Left;
	public GameObject Knee_Left;
	public GameObject Ankle_Left;
	public GameObject Foot_Left;
	public GameObject Hip_Right;
	public GameObject Knee_Right;
	public GameObject Ankle_Right;
	public GameObject Foot_Right;
	
	private GameObject[] _bones; 

	private BoneMask Mask = BoneMask.Extremities;
	
	public float scale = 1.0f;
	
	void Start ()
    {
		_bones = new [] {Hip_Center, Spine, Shoulder_Center, Head,
			Shoulder_Left, Elbow_Left, Wrist_Left, Hand_Left,
			Shoulder_Right, Elbow_Right, Wrist_Right, Hand_Right,
			Hip_Left, Knee_Left, Ankle_Left, Foot_Left,
			Hip_Right, Knee_Right, Ankle_Right, Foot_Right};
	    for (int i = 0; i < _bones.Length; ++i)
	        _bones[i].SetActive(IsBoneActive(i));
    }
	
	void Update ()
	{
	    if (!SkeletonWrapper.singleton.PollSkeleton()) return;
	    for( int ii = 0; ii < _bones.Length; ii++)
	    {
	        if (!IsBoneActive(ii)) continue;
	        _bones[ii].transform.localPosition = new Vector3(
	            SkeletonWrapper.singleton.bonePos[ii].x * scale,
	            SkeletonWrapper.singleton.bonePos[ii].y * scale,
	            SkeletonWrapper.singleton.bonePos[ii].z * scale);
	    }
	}

    private bool IsBoneActive(int index)
    {
        return ((uint) Mask & (uint) (1 << index)) > 0;
    }
}
