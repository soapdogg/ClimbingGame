using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration2 : MonoBehaviour {

    Vector3 dot_hip_pos;
    Vector3 dot_hand_pos;
    Vector3 skele_hip_pos;
    Vector3 skele_hand_right_pos;

    int numPolls = -1;
    int numAveragedPoints = 10;
    private List<Vector3> boneHipPositions = new List<Vector3>();
    private List<Vector3> boneHandPositions = new List<Vector3>();
    private List<Vector3> tempHipPositions = new List<Vector3>();
    private List<Vector3> tempHandPositions = new List<Vector3>();
    // Use this for initialization
    void Start () {

        dot_hip_pos = GameObject.Find("HipDot").transform.position;
        dot_hand_pos = GameObject.Find("HandDot").transform.position;
        skele_hip_pos = GameObject.Find("00_Hip_Center").transform.position;
        skele_hand_right_pos = GameObject.Find("23_Hand_Right").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (numPolls != -1)
        {

            tempHipPositions.Add(skele_hip_pos);
            tempHandPositions.Add(skele_hand_right_pos);
            Debug.Log("added: " + skele_hip_pos);

            numPolls++;
            if (numPolls == numAveragedPoints)
            {
                skele_hip_pos = AveragePoints(tempHipPositions);
                skele_hand_right_pos = AveragePoints(tempHandPositions);
                Calibrate(skele_hip_pos, skele_hand_right_pos);
                numPolls = -1;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.name == "HipDot")
            {
                numPolls = 0;

            }
        }
    }

    Vector3 AveragePoints(List<Vector3> points)
    {
        Debug.Log("point length: " + points.Count);
        Vector3 averaged = new Vector3(0, 0, 0);
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 point = points[i];
            averaged.x += point.x;
            averaged.y += point.y;
            averaged.z += point.z;
        }
        averaged.x /= points.Count;
        averaged.y /= points.Count;
        averaged.z /= points.Count;
        Debug.Log("Averaged: " + averaged);
        return averaged;
    }

    void Calibrate(Vector3 skele_hip_pos, Vector3 skele_hand_right_pos){
        Debug.Log("Skeleton hip position: " + skele_hip_pos);
        Debug.Log("Skeleton hand position: " + skele_hand_right_pos);
        Debug.Log("Dot hip position: " + dot_hip_pos);
        Debug.Log("Dot hand position: " + dot_hand_pos);

        float scaleX = (dot_hip_pos.x - dot_hand_pos.x) / (skele_hip_pos.x - skele_hand_right_pos.x);
        float scaleY = (dot_hip_pos.y - dot_hand_pos.y) / (skele_hip_pos.y - skele_hand_right_pos.y);
        Vector3 translate_scale = new Vector3(scaleX, scaleY, 1);

        Vector3 offset = new Vector3(dot_hip_pos.x - skele_hip_pos.x * translate_scale.x,
                                    dot_hip_pos.y - skele_hip_pos.y * translate_scale.y);


        Debug.Log("Offset: " + offset);
        Debug.Log("Translation scale: " + translate_scale);

        var skeleton = GameObject.Find("KinectPointMan");
        // TODO: note that the KinectPointMan by default has a z position of -5.
        skeleton.transform.position = offset;
        skeleton.transform.localScale = translate_scale;
    }
    

}
