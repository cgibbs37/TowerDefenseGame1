using UnityEngine;

public class Enemy : MonoBehaviour
{
    //sets the movement speed of the enemy
    public float speed = 10f;

    private Transform target;
    private int waypointIndex = 0;

    //sets the starting spawnpoint of the enemy objects
    void Start ()
    {
        target = Waypoints.points[0];
    }

    //Updates enemy waypoints to find correct pathing through the games end point.
    void Update ()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    //Gets the waypoint info, if the waypoint is equal to the endpoint, the enemy vanishes like a typical tower defense game.
    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length -1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }
}
