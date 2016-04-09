using UnityEngine;
using System.Collections;

public class Airship : MonoBehaviour {

    private PolygonCollider2D outsideOfTheShip;
    private PolygonCollider2D theCabin;
    
    public void Start()
    {
        outsideOfTheShip = transform.FindChild("OutsideOfShip").GetComponent<PolygonCollider2D>();
        theCabin = transform.FindChild("Cabin").GetComponent<PolygonCollider2D>();
    }

    public void Update()
    {
        //Debug.DrawLine(new Vector2(0, 0), GetRandomPointOnAirship(), Color.red, 0.2f);
    }

    public Vector2 GetRandomPointOnAirship()
    {
        bool isInside = false;
        Vector2 point;
        do
        {
            point = GetRandomPointOnTheScreenInWorldSpace();
            isInside = IsPointOnAirship(point);
        }
        while (!isInside);

        return point;
    }

    private Vector2 GetRandomPointOnTheScreenInWorldSpace()
    {
        int x = Random.Range(0, Screen.width);
        int y = Random.Range(0, Screen.height);
        return Camera.main.ScreenToWorldPoint(new Vector2(x,y));
    }

    public bool IsPointOnAirship(Vector2 point)
    {
        if (outsideOfTheShip.bounds.Contains(point))
        {
            return false;
        }
        if (theCabin.bounds.Contains(point))
        {
            return false;
        }
        return true;
    }

    public void ApplyPatch(Vector2 location)
    {
        print("Applying patch at " + location);
        // TODO!
    }
}
