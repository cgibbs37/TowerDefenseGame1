using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    //Sets bullet speed of the bullet object
    public float speed = 70f;
    
    //Seek method follows the target of the turret game object.
    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame, and checks if the enemy object is hit.
    void Update()
    {
        //If the enemy target dies or is removed from hitting the endpoint, the bullet object is destroyed.
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    //Debug for if the enemy target was hit by the turret game object.
    void HitTarget()
    {
        Debug.Log("We Hit Something");
    }
}
