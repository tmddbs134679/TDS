using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float maxSpeed = 200f;   // 최대 속도
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = maxSpeed;

        // SpeedManager의 OnSpeedChange 이벤트 구독
        SpeedManager.Instance.OnSpeedChange += UpdateSpeed;
    }

    private void OnDestroy()
    {
        // 오브젝트가 파괴될 때 이벤트 구독 해제
        SpeedManager.Instance.OnSpeedChange -= UpdateSpeed;
    }

    private void Update()
    {
        // 바퀴 회전
        transform.Rotate(Vector3.forward, currentSpeed * Time.deltaTime);
    }

    /// <summary>
    /// SpeedManager에서 collisionCount가 바뀔 때 호출되는 메서드
    /// </summary>
    /// <param name="collisionCount">현재 트럭-몬스터 충돌 횟수</param>
    private void UpdateSpeed(int collisionCount)
    {
        // 충돌 횟수(collisionCount)에 따라 속도 감소
        // 예: collisionCount * 2f 만큼 감소
        currentSpeed = Mathf.Max(0, maxSpeed - collisionCount * 2f);
        Debug.Log($"[Wheel] Speed updated: {currentSpeed}");
    }
}
