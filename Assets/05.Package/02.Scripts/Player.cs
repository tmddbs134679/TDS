using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] HealthUI healthUI;
    private Transform nearestEnemy;
    public float attackDamage = 50f;
    private EnemyFinder enemyFinder; // 가장 가까운 적 찾기
    private GameObject healthUIObj;

    void Awake()
    {
        enemyFinder = GetComponent<EnemyFinder>();

        health = GetComponent<Health>();
        healthUI = GetComponent<HealthUI>();
        healthUIObj = transform.Find("HPPanel").gameObject;

        if (healthUI != null)
        {
            health.HealthChanged += healthUI.UpdateHealthUI;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
            if (!healthUIObj.activeSelf)
            {
                healthUIObj.SetActive(true);
            }
        }
    }

    public Transform GetNearestEnemy()
    {
        return enemyFinder?.GetNearestEnemy();
    }
}
