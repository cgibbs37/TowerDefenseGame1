using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    
    //Attributes for the turret game object.
    [Header("Attributes")]

    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    //Rotation of the turret barrel of the turret game object.
    public Transform partToRotate;
    public float turnSpeed = 10f;

    //Bullet game object and firing location on the turret objects barrel.
    public GameObject bulletPrefab;
    public Transform firePoint;

    //Updating which enemy to target.
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

    //Targets enemy based on distance of the range variable on the turret game object.
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    
    void Update()
    {
        if (target == null)
        {
            return;
        }

        //Target Lock-On, makes the turret point at the target it is looking at.
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

        //Firing cooldown based on firerate and firecooldown attribute.
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    //Spawns bullet object and makes it follow enemy object that the turret object is targeting.
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    //Shows range of turret game object with a red circle, based on range attribute of turret.
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
