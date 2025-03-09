using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Health health;
    [SerializeField]private Slider healthBar; // UI �̹��� (ü�¹�)

    private void Awake()
    {
        if (health != null)
        {
            health.HealthChanged += UpdateHealthUI;
            UpdateHealthUI(health.curHealth, health.maxHealth);
        }
    }

    public void UpdateHealthUI(float current, float max)  // ü�¹� ������Ʈ
    {
        if (healthBar != null)
        {
            healthBar.value = current / max; 
        }
    }
}
