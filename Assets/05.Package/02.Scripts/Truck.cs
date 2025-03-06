using UnityEngine;

public class Truck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 몬스터와 충돌 시
        if (collision.collider.CompareTag("Monster"))
        {
           
            // SpeedManager에 알려서 충돌 횟수 증가
            SpeedManager.Instance.IncreaseCollisionCount();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 몬스터와 충돌 종료 시
        if (collision.collider.CompareTag("Monster"))
        {
          
            // SpeedManager에 알려서 충돌 횟수 감소
            SpeedManager.Instance.DecreaseCollisionCount();
        }
    }
}
