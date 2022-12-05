using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bezier : MonoBehaviour
{
    [HideInInspector] public List<Vector3> points = new List<Vector3>();
    public event Action OnEditCurve;

    
    public bool showGizmos = true;
    public bool showCurveGizmo = true;

    bool autoSetControl = false;
    public bool AutoSetControl
    {
        get
        {
            return autoSetControl;
        }

        set
        {
            if(autoSetControl != value)
            {
                autoSetControl = value;

                if (autoSetControl)
                {
                    AutoSetAllControlPoints();
                    OnEditCurve?.Invoke();                                                                            
                }
            }
        }
    }

    public enum LockMode { Free, Locked2D, LockedTopDown }
    LockMode lockMode;

    public LockMode CurveLockMode
    {
        get { return lockMode; }

        set 
        {
            if (value != lockMode)
            {
                lockMode = value;


                switch (lockMode)
                {

                    case LockMode.Locked2D:
                        LockZ();
                        break;
                    case LockMode.LockedTopDown:
                        AlignYPosToAverage();
                        break;

                }

            }
        }
    }

    public void ResetCurve()
    {
        
        points.Clear();
        points.Add(Vector3.left + transform.position);
        points.Add((Vector3.forward + Vector3.left) * 0.5f + transform.position);
        points.Add((Vector3.right + Vector3.back) * 0.5f + transform.position); 
        points.Add(Vector3.right + transform.position);

        OnEditCurve = null;
        
    }
    /// <summary>
    /// Returns the Bezier point in world position
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public Vector3 this[int i] => transform.TransformPoint(points[i]); 

    public int NumSegments => points.Count/3;

    public void AddSegment(Vector3 anchorPos)
    {

        Vector3 previousAnchor = points[points.Count - 1];
        Vector3 previousTangent = points[points.Count - 2];

        float dst = Vector3.Distance(previousTangent, previousAnchor);
        Vector3 dir = (previousAnchor - previousTangent).normalized;
        points.Add(previousAnchor + dir * dst);

        
        points.Add(anchorPos + (Vector3.right + Vector3.back) * 0.5f);
        points.Add(anchorPos);
        OnEditCurve?.Invoke();
    }


    public void RemoveSegment(int anchorIdx)
    {
        if (anchorIdx % 3 != 0 || NumSegments == 1) return;



        //First Segment
        if (anchorIdx == 0)
        {
            points.RemoveRange(anchorIdx, 2);
        }

        //Last Segment
        else if (anchorIdx == points.Count -1)
        {
            points.RemoveRange(anchorIdx - 1, 2);
        }

        //All middle segments
        else
        {
            points.RemoveRange(anchorIdx - 1, 3);
        }

        OnEditCurve?.Invoke();
    }

    private void Reset()
    {
        ResetCurve();
    }

    public void MovePoint(int i, Vector3 pos)
    {

        //If Moving an Anchor point
        if (i % 3 == 0)
        {


            if (i < points.Count -1)
            {
                Vector3 offset = points[i + 1] - points[i];
                
                points[i + 1] = pos + offset;
            }

            if (i > 0)
            {
                Vector3 offset = points[i -1] - points[i];
                points[i - 1] = pos + offset;
            }

            points[i] = pos;
        }

        //If moving Control Point
        else if (!autoSetControl)
        {
            //For first and last control point
            if (i == 1 || i == points.Count -2)
            {
                points[i] = pos;
            }

            //For the rest of the control points
            else
            {
                //Moving draged control point
                points[i] = pos;

                int other = GetCorrespondingControlIdx(i);
                int anchor = GetCorrespondingAnchorIdx(i);

                float dst = Vector3.Distance(points[anchor], points[other]);
                Vector3 dir = (points[anchor] - points[i]).normalized;

                points[other] = points[anchor] + dir * dst;
            }
        }

        if (autoSetControl && i % 3 == 0)
        {
            AutoSetAllAffectedControlPoints(i);
        }

        OnEditCurve?.Invoke();
    }

    public int GetCorrespondingAnchorIdx(int i)
    {
        if ((i + 1) % 3 == 0) return i + 1;

        else if ((i - 1) % 3 == 0) return i - 1;

        else return i;
    }

    public int GetCorrespondingControlIdx(int i)
    {
        if ((i + 1) % 3 == 0) return i +2;

        else return i - 2;
    }

    public void ResetEditEvent()
    {
        OnEditCurve = null;
    }

    public BezierPoint GetPoint(int segment, float t)
    {
        Vector3 p0 = points[segment * 3];
        Vector3 p1 = points[segment * 3 + 1];
        Vector3 p2 = points[segment * 3 + 2];
        Vector3 p3 = points[segment * 3 + 3];

        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 point = Vector3.Lerp(d, e, t);
        Vector3 tangent = (e - d).normalized;

        return new BezierPoint(point, Quaternion.LookRotation(tangent));
    }



    public void AutoSetControlPoints(int anchorIndex)
    {
        Vector3 anchorPos = points[anchorIndex];
        Vector3 dir = Vector3.zero;
        float[] neighbourDistances = new float[2];

        if (anchorIndex - 3 >= 0 )
        {
            Vector3 offset = points[anchorIndex - 3] - anchorPos;
            dir += offset.normalized;
            neighbourDistances[0] = offset.magnitude;
        }

        if (anchorIndex + 3 < points.Count)
        {
            Vector3 offset = points[anchorIndex + 3] - anchorPos;
            dir -= offset.normalized;
            neighbourDistances[1] = -offset.magnitude;
        }

        dir.Normalize();

        for (int i = 0; i < 2; i++)
        {
            int controlIndex = anchorIndex + i * 2 - 1;

            if (controlIndex >= 0 && controlIndex < points.Count)
            {
                points[controlIndex] = anchorPos + dir * neighbourDistances[i] * 0.5f;
            }
        }
    }

    void AutoSetStartAndEndControls()
    {
        points[1] = (points[0] + points[2]) * 0.5f;
        points[points.Count - 2] = (points[points.Count - 1] + points[points.Count - 3]) * 0.5f;

    }

    void AutoSetAllControlPoints()
    {
        for (int i = 0; i < points.Count; i+= 3)
        {
            AutoSetControlPoints(i);
        }

        AutoSetStartAndEndControls();
    }

    void AutoSetAllAffectedControlPoints(int updatedIdx)
    {
        for (int i = updatedIdx -3; i < updatedIdx +3; i+= 3)
        {
            if (i >= 0 && i < points.Count)
            {
                AutoSetControlPoints(i);
            }
        }

        AutoSetStartAndEndControls();
    }

    public void AlignYPosToAverage()
    {
        float totalY = 0;

        for (int i = 0; i < points.Count; i+= 3)
        {
            totalY += points[i].y;
        }


        float average = totalY / points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            points[i] = new Vector3(points[i].x, average, points[i].z);
        }

        OnEditCurve?.Invoke();
    }

    public void LockZ()
    {
        for (int i = 0; i < points.Count; i++)
        {
            points[i] = new Vector3(points[i].x, points[i].y, 0);
        }

        OnEditCurve?.Invoke();
    }

}

public struct BezierPoint
{
    public Vector3 point;
    public Quaternion rotation;

    public BezierPoint(Vector3 point, Quaternion rotation)
    {
        this.point = point;
        this.rotation = rotation;
    }
}
