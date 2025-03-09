using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gun : MonoBehaviour
{
    public Transform targetPoint;
    public int bulletSpeed = 50;
    private float fireTime = 0.5f;
    private float nextfireTime = 0;
    private Player player;


    private void Awake()
    {
        player = FindObjectOfType<Player>(); 
    }

    private void Update()
    {
        if(targetPoint != null)
        {
            AimAtTarget(targetPoint);
        }
      
        if (Time.time >= nextfireTime)
        {
            targetPoint = player.GetNearestEnemy();
        
            if(targetPoint != null )
            {
                Shoot(targetPoint);
            }

            nextfireTime = Time.time + fireTime; 
        }
    }


    void Shoot(Transform target)
    {
        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = player.transform.position;

        // 총알이 적을 향하도록 회전
        Vector2 direction = (targetPoint.position - player.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        bullet.GetComponent<Bullet>().speed = bulletSpeed;
    }

    private void AimAtTarget(Transform target)
    {
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z; 

        transform.right = targetPosition - transform.position;
    }
}
