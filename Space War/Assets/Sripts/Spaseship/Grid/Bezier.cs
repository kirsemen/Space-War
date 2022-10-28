using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Bezier : MonoBehaviour
{
    private BezierPoint[] objects = new BezierPoint[0];
    public bool draw = true;
    public float position = 0;

    private void Awake()
    {
        objects = new BezierPoint[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            objects[i] = transform.GetChild(i).GetComponent<BezierPoint>();
    }
    private void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            objects = new BezierPoint[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
                objects[i] = transform.GetChild(i).GetComponent<BezierPoint>();
        }
    }
    private void OnDrawGizmos()
    {

        if (draw)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < objects.Length - 1; i++)
            {
                int i1 = i + 1;
                int sigmentsNumber = 20;
                Vector3 preveousePoint = objects[i].transform.position;

                for (int j = 0; j < sigmentsNumber + 1; j++)
                {
                    float paremeter = (float)j / sigmentsNumber;
                    Vector3 point = GetPoint(objects[i], objects[i1], paremeter);
                    Gizmos.DrawLine(preveousePoint, point);
                    preveousePoint = point;
                }

            }
        }
    }
    public static Vector2 GetLinesCrossVector(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
    {
        float xo = A.x, yo = A.y;
        float p = B.x - A.x, q = B.y - A.y;

        float x1 = C.x, y1 = C.y;
        float p1 = D.x - C.x, q1 = D.y - C.y;

        float x = (xo * q * p1 - x1 * q1 * p - yo * p * p1 + y1 * p * p1) /
            (q * p1 - q1 * p);
        float y = (yo * p * q1 - y1 * p1 * q - xo * q * q1 + x1 * q * q1) /
            (p * q1 - p1 * q);

        return new Vector2(x, y);
    }

    public static Vector3 GetPointByZPosition(Bezier b, float l)
    {
        
        for (int i = 0; i < b.objects.Length - 1; i++)
        {
            int i1 = i + 1;
            if (b.objects[i].transform.position.z > l && l > b.objects[i1].transform.position.z)
            {
                var bp1 = b.objects[i];
                var bp2 = b.objects[i1];

                var p0 = bp1.transform.position.z;
                var p1 = bp1.transform.position.z + bp1.SecondBezierPoint.z;
                var p2 = bp2.transform.position.z - bp2.SecondBezierPoint.z;
                var p3 = bp2.transform.position.z;


                int sigmentsNumber = 100;
                float[] ls = new float[sigmentsNumber];
                for (int t = 0; t < sigmentsNumber; t++)
                {
                    ls[t] = GetPoint(p0, p1, p2, p3, (float)t / sigmentsNumber);
                }
                float time = 0;
                float min = MathF.Abs(ls[0] - l);
                for (int t = 1; t < sigmentsNumber; t++)
                {
                    if (MathF.Abs(ls[t] - l) < min)
                    {
                        min = MathF.Abs(ls[t] - l);
                        time = (float)t / sigmentsNumber;
                    }
                }
                return GetPoint(bp1,bp2,time);
            }
        }

        return Vector3.zero;
    }
    public static Vector3 GetDerivativeByZPosition(Bezier b, float l)
    {

        for (int i = 0; i < b.objects.Length - 1; i++)
        {
            int i1 = i + 1;
            if (b.objects[i].transform.position.z > l && l > b.objects[i1].transform.position.z)
            {
                var bp1 = b.objects[i];
                var bp2 = b.objects[i1];

                var p0 = bp1.transform.position.z;
                var p1 = bp1.transform.position.z + bp1.SecondBezierPoint.z;
                var p2 = bp2.transform.position.z - bp2.SecondBezierPoint.z;
                var p3 = bp2.transform.position.z;


                int sigmentsNumber = 100;
                float[] ls = new float[sigmentsNumber];
                for (int t = 0; t < sigmentsNumber; t++)
                {
                    ls[t] = GetPoint(p0, p1, p2, p3, (float)t / sigmentsNumber);
                }
                float time = 0;
                float min = MathF.Abs(ls[0] - l);
                for (int t = 1; t < sigmentsNumber; t++)
                {
                    if (MathF.Abs(ls[t] - l) < min)
                    {
                        min = MathF.Abs(ls[t] - l);
                        time = (float)t / sigmentsNumber;
                    }
                }
                return GetFirstDerivative(bp1, bp2, time);
            }
        }

        return Vector3.zero;
    }

    public static Vector3 GetPoint(BezierPoint bp1, BezierPoint bp2, float t)
    {
        var p0 = bp1.transform.position;
        var p1 = bp1.transform.position + bp1.SecondBezierPoint;
        var p2 = bp2.transform.position - bp2.SecondBezierPoint;
        var p3 = bp2.transform.position;


        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
    }
    public static float GetPoint(float p0, float p1, float p2, float p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
    }

    public static Vector3 GetFirstDerivative(BezierPoint bp1, BezierPoint bp2, float t)
    {
        var p0 = bp1.transform.position;
        var p1 = bp1.transform.position + bp1.SecondBezierPoint;
        var p2 = bp2.transform.position - bp2.SecondBezierPoint;
        var p3 = bp2.transform.position;

        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }
}
