using UnityEngine;
using System.Collections;

public class PointController : MonoBehaviour {

	public int points  = 0;

	public void GivePoints(int addPoints)
    {
        points += addPoints;
    }

    public int GetPoints()
    {
        return points;
    }

    public void ResetPoints()
    {
        points = 0;
    }

}
