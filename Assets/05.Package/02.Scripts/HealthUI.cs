using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Health health;
    [SerializeField]private Slider healthBar; // UI 이미지 (체력바)

    private void Awake()
    {
        if (health != null)
        {
            health.HealthChanged += UpdateHealthUI;
            UpdateHealthUI(health.curHealth, health.maxHealth);
        }
    }

    public void UpdateHealthUI(float current, float max)  // 체력바 업데이트
    {
        if (healthBar != null)
        {
            healthBar.value = current / max; 
        }
    }
}
