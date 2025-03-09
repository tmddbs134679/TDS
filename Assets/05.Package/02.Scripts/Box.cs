using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IDamage
{
    private Health health;
    private GameObject healthUIObj;
    private bool isInvincible = false;
    private HealthUI healthUI;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        health = GetComponent<Health>();
        healthUIObj = transform.Find("HPPanel").gameObject;
        healthUI = GetComponent<HealthUI>();

        if (healthUI != null)
        {
            health.HealthChanged += healthUI.UpdateHealthUI;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Monster"))
        {
            if (!healthUIObj.activeSelf)
            {
                healthUIObj.SetActive(true);
            }
        }
    }


    public void TakeDamage(float damage)
    {
        if (isInvincible) 
            return;

        health.TakeDamage(damage);

        ActivateInvincibility(2f).Forget();
    }

    private async UniTask ActivateInvincibility(float duration)
    {
        isInvincible = true;

        float blinkInterval = 0.2f; // 0.2초 간격으로 깜박임
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // 깜박임 효과
            await UniTask.Delay((int)(blinkInterval * 1000)); // 0.2초 대기
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true; // 원래 상태로 복구
        isInvincible = false;
     
    }




}
