using UnityEngine;
using System.Collections.Generic;

public class EnemyFinder : MonoBehaviour
{
    public Transform target; // �÷��̾�
    private HashSet<Transform> nearbyEnemies = new HashSet<Transform>(); 
    private Transform nearestEnemy; // ���� ����� ��
    private float updateInterval = 0.2f; // �˻� �ֱ� (0.2��)
    private float nextUpdateTime = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            nearbyEnemies.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            nearbyEnemies.Remove(other.transform); 
        }
    }

    private void Update()
    {
        if (Time.time >= nextUpdateTime)
        {
            nearestEnemy = FindNearestEnemy(); 
            nextUpdateTime = Time.time + updateInterval;
        }
    }

    private Transform FindNearestEnemy()
    {
        if (nearbyEnemies.Count == 0 || target == null)
            return null;

        Transform nearest = null;
        float minDistSqr = Mathf.Infinity; 

        foreach (Transform enemy in nearbyEnemies)
        {
            if (enemy == null) continue;

            float distSqr = (target.position - enemy.position).sqrMagnitude;
            if (distSqr < minDistSqr)
            {
                minDistSqr = distSqr;
                nearest = enemy;
            }
        }

        return nearest;
    }

    public Transform GetNearestEnemy()
    {
        if(nearestEnemy != null)
        return nearestEnemy; 
        else
            return null;
    }
}
