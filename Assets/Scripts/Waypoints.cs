using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    //Method to make waypoints for the enemy to run to.

    void Awake ()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
