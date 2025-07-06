using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    public static ProjectileLine S;

    [SerializeField] private float minDist = 0.1f;

    private GameObject _poi;

    private LineRenderer line;
    private List<Vector3> points;

    public GameObject poi
    {
        get => _poi;
        set
        {
            _poi = value;
            if (_poi)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    public Vector3 lastPoint
    {
        get
        {
            if (points == null) return Vector3.zero;
            return points[points.Count - 1];
        }
    }

    private void Awake()
    {
        S = this; // Set the singleton
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }

    private void FixedUpdate()
    {
        if (!poi)
            if (FollowCam.POI)
            {
                if (FollowCam.POI.tag == "Projectile")
                    poi = FollowCam.POI;
                else
                    return; // Return if we didn't find a poi
            }

        AddPoint();

        if (!FollowCam.POI)
            poi = null;
    }

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {
        if (!_poi) return;
        var pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
            return;
        if (points.Count == 0)
        {
            // If this is the launch point...
            var launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }
        else
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }
}