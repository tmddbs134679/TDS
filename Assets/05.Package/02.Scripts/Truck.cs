using UnityEngine;

public class Truck : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 몬스터와 충돌 시
        if (collision.CompareTag("Monster"))
        {
            // SpeedManager에 알려서 충돌 횟수 증가
            SpeedManager.Instance.IncreaseCollisionCount();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // SpeedManager에 알려서 충돌 횟수 감소
            SpeedManager.Instance.DecreaseCollisionCount();
        }
    }


}
