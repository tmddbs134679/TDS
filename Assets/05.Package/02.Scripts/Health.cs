using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour , IDamage
{
    public float curHealth;
    [SerializeField] public int maxHealth = 100;

    public delegate void HealthChange(float current, float max);
    public event HealthChange HealthChanged;

    private void Awake()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        curHealth = Mathf.Clamp(curHealth - damage, 0, maxHealth);
        HealthChanged?.Invoke(curHealth, maxHealth);

        if (curHealth <= 0)
            DestroyObj();
    }

    private void DestroyObj()
    {
        gameObject.SetActive(false);
    }
}
