﻿using UnityEngine;
using System.Collections;

public class Airship : MonoBehaviour {
    public float altitudeInMeters = 3000;

    private PolygonCollider2D inSideTheShip;
    private BoxCollider2D boxCollider;

    public void Start()
    {
        inSideTheShip = transform.FindChild("Graphics").GetComponent<PolygonCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
        Debug.DrawLine(new Vector2(0, 0), GetRandomPointOnAirship(), Color.green, 0.2f); //Debugging for where the generated point is located. 
    }

    public Vector2 GetRandomPointOnAirship()
    {
        bool isInside = false;
        Vector2 point;
        do
        {
            point = GetRandomPointInBoxCollider();
            isInside = IsPointOnAirship(point);
        }
        while (!isInside);
        Debug.Log(isInside);
        return point;
    }

    private Vector2 GetRandomPointInBoxCollider()
    {
        Bounds bounds = boxCollider.bounds;
        Vector3 center = bounds.center;

        var x = Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
        var y = Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);

        return new Vector2(x,y);
    }
    public bool IsPointOnAirship(Vector2 point)
    {
        return inSideTheShip.OverlapPoint(point);
    }

    public void ReduceAltitude(float altitudeLossInMeters)
    {
        altitudeInMeters -= altitudeLossInMeters;
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (altitudeInMeters > 0)
            return;
        print("Game over!");
        // TODO!

    }

    public void ApplyPatch(Vector2 location)
    {
        print("Applying patch at " + location);
        // TODO!
    }
}
