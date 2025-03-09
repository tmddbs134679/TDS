using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float maxSpeed = 200f;   // 최대 속도
    [SerializeField]private float currentSpeed;
    private int weight = 40;

    private void Start()
    {
        currentSpeed = maxSpeed;
        SpeedManager.Instance.OnSpeedChange += UpdateSpeed;
    }

    private void OnDestroy()
    {
        SpeedManager.Instance.OnSpeedChange -= UpdateSpeed;
    }

    private void Update()
    {
        // 바퀴 회전
        transform.Rotate(Vector3.back, currentSpeed * Time.deltaTime);
    }

    private void UpdateSpeed(int collisionCount)
    {
        currentSpeed = Mathf.Max(0, maxSpeed - collisionCount * weight);
    }
}
