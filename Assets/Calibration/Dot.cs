using UnityEngine;
using System.Collections.Generic;
using System.IO;
using MathNet.Numerics.LinearAlgebra;

public class Dot : MonoBehaviour {

    private static List<Point> rawBonePositions;
    private static List<Point> screenPositions;
    private List<Point> tempBonePositions;
    public GameObject dot;
    private bool merp = false;
    private bool canclick = true;
    private float clickTime = 0;
    private int numPolls = -1;
    private int handindex;
    public static int numAveragedPoints = 10;
    public static bool calculateWithFixedZ = false;
    public static bool mapWithFixedZ = false;
    public static bool useHip = false;
    public static int pointsToUse = 12;
    private int pointScaling = 1;
    

	// Use this for initialization
	void Start () {
        rawBonePositions = new List<Point>();
        screenPositions = new List<Point>();
        tempBonePositions = new List<Point>();
        //pointScaling = SkeletonWrapper.Instance.pointScaling;
	}
	
	// Update is called once per frame
	void Update () {
        if (numPolls != -1)
        {

            tempBonePositions.Add(new Point(pointScaling * SkeletonWrapper.Instance.bonePos[0, handindex].x,
                    pointScaling * SkeletonWrapper.Instance.bonePos[0, handindex].y,
                    pointScaling * SkeletonWrapper.Instance.bonePos[0, handindex].z));
            numPolls++;
            if(numPolls == numAveragedPoints)
            {
                rawBonePositions.Add(AveragePoints(tempBonePositions));
                tempBonePositions.Clear();
                numPolls = -1;
                dot.SetActive(false);
            }
        }
        if (Input.GetMouseButtonDown(0) && canclick)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit) && hit.transform.name == dot.name)
            {
                handindex = 11;
                GameObject hand = GameObject.Find("13_Hand_Left");
                if (rawBonePositions.Count >= 6)
                {
                    hand = GameObject.Find("23_Hand_Right");
                    handindex = 7;
                }
                if (useHip)
                {
                    hand = GameObject.Find("0_Hip_Center");
                    handindex = 0;
                }
                MeshRenderer renderer = hand.GetComponent<MeshRenderer>();
                renderer.material = Resources.Load("fuck this", typeof(Material)) as Material;
                Point addedPoint = new Point(pointScaling * SkeletonWrapper.Instance.bonePos[0, handindex].x,
                        pointScaling * SkeletonWrapper.Instance.bonePos[0, handindex].y,
                        pointScaling * SkeletonWrapper.Instance.bonePos[0, handindex].z);
                tempBonePositions.Add(addedPoint);
                Point screenPos = new Point(hit.transform.position.x,
                        hit.transform.position.y,
                        hit.transform.position.z);
                screenPositions.Add(screenPos);
                numPolls = 1;


                Debug.Log("added: " + addedPoint);
                canclick = false;
                clickTime = Time.time;
               
            }
        }
        if(Time.time > clickTime + 1 && !canclick)
        {
            Debug.Log("can click again");
            canclick = true;
        }
        if(rawBonePositions.Count == pointsToUse && !merp)
        {
            if (calculateWithFixedZ)
            {
                CalculateFixedZ(rawBonePositions);
            }
            Matrix<float> mapMatrix = FindTransformMatrix(rawBonePositions, screenPositions);
            SkeletonWrapper.Instance.calibMatrix = mapMatrix;
            merp = true;

            //TODO Make this better
            StreamWriter file = new StreamWriter(@"C:\Users\rniemo\Desktop\Exaample.txt");
            for (int i = 0; i < pointsToUse; i++)
            {
                file.WriteLine(rawBonePositions[i].ToString());
                Debug.Log(rawBonePositions[i]);
                
                
            }
            file.WriteLine();
            for (int i = 0; i < pointsToUse; i++)
            {
                file.WriteLine(screenPositions[i].ToString());
                Debug.Log(screenPositions[i]);
            }

            file.WriteLine(mapMatrix.ToString());
            TestMatrix(rawBonePositions, screenPositions, mapMatrix, file);
            file.Close();
        }
    }

    static void CalculateFixedZ(List<Point> points)
    {
        float averageZ = 0;
        for(int i = 0; i < points.Count; i++)
        {
            averageZ += points[i].z;
        }
        averageZ /= points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            points[i].z = averageZ;
        }
        Debug.Log("FIXED Z VALUE: " + averageZ);
        if (mapWithFixedZ)
        {
            SkeletonWrapper.Instance.fixedZ = averageZ;
        }
    }

    static Point AveragePoints(List<Point> points)
    {
        Debug.Log("point length: " + points.Count);
        Point averaged = new Point(0, 0, 0);
        for(int i = 0; i < points.Count; i++)
        {
            Point point = points[i];
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

    static void TestMatrix(IList<Point> kinectPoints, IList<Point> screenPoints, Matrix<float> mapMatrix, StreamWriter file)
    {
        file.WriteLine("\nTESTING: ");
        for (int i = 0; i < kinectPoints.Count; i++)
        {
            Vector<float> kinectPoint = kinectPoints[i].toVector();
            Vector<float> screenPoint = screenPoints[i].toVector();
            Vector<float> mapped = mapMatrix.Multiply(kinectPoint);
            file.WriteLine("Mapped w: " + mapped[2]);
            mapped = mapped.Divide(mapped[2]);
            
            file.WriteLine("goal: " + screenPoint.ToString());
            file.WriteLine("mapped: " + mapped.ToString());

        }
    }

    static Matrix<float> FindTransformMatrix(IList<Point> kinectPoints, IList<Point> screenPoints)
    {
        Matrix<float> A = Matrix<float>.Build.Dense(kinectPoints.Count * 2, 12);
        for (int i = 0; i < kinectPoints.Count; i++)
        {
            Point kinectPoint = kinectPoints[i];
            Point screenPoint = screenPoints[i];
            A[i * 2, 0] = kinectPoint.x;
            A[i * 2, 1] = kinectPoint.y;
            A[i * 2, 2] = kinectPoint.z;
            A[i * 2, 3] = 1;
            A[i * 2, 8] = kinectPoint.x * -screenPoint.x;
            A[i * 2, 9] = kinectPoint.y * -screenPoint.x;
            A[i * 2, 10] = kinectPoint.z * -screenPoint.x;
            A[i * 2, 11] = -screenPoint.x;

            A[i * 2 + 1, 4] = kinectPoint.x;
            A[i * 2 + 1, 5] = kinectPoint.y;
            A[i * 2 + 1, 6] = kinectPoint.z;
            A[i * 2 + 1, 7] = 1;
            A[i * 2 + 1, 8] = kinectPoint.x * -screenPoint.y;
            A[i * 2 + 1, 9] = kinectPoint.y * -screenPoint.y;
            A[i * 2 + 1, 10] = kinectPoint.z * -screenPoint.y;
            A[i * 2 + 1, 11] = -screenPoint.y;
        }

        var X = A.Transpose().Multiply(A);
        var evd = X.Evd(Symmetricity.Unknown);
        var vector = evd.EigenVectors.Column(0);

        IList<Vector<float>> rows = new List<Vector<float>>();
        for (int i = 0; i < 3; i++)
        {
            rows.Add(vector.SubVector(i * 4, 4));
        }
        return Matrix<float>.Build.DenseOfRowVectors(rows);
    }

    class Point
    {

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Point(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector<float> toVector()
        {
            return Vector<float>.Build.DenseOfArray(new float[] { x, y, z, 1 });
        }

        override
        public string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }
    }


}

