using UnityEngine;

public class Truck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���Ϳ� �浹 ��
        if (collision.collider.CompareTag("Monster"))
        {
           
            // SpeedManager�� �˷��� �浹 Ƚ�� ����
            SpeedManager.Instance.IncreaseCollisionCount();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ���Ϳ� �浹 ���� ��
        if (collision.collider.CompareTag("Monster"))
        {
          
            // SpeedManager�� �˷��� �浹 Ƚ�� ����
            SpeedManager.Instance.DecreaseCollisionCount();
        }
    }
}
