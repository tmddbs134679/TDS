using UnityEngine;

public class Truck : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���Ϳ� �浹 ��
        if (collision.CompareTag("Monster"))
        {
            // SpeedManager�� �˷��� �浹 Ƚ�� ����
            SpeedManager.Instance.IncreaseCollisionCount();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // SpeedManager�� �˷��� �浹 Ƚ�� ����
            SpeedManager.Instance.DecreaseCollisionCount();
        }
    }


}
