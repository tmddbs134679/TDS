using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float maxSpeed = 200f;   // �ִ� �ӵ�
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
        // ���� ȸ��
        transform.Rotate(Vector3.back, currentSpeed * Time.deltaTime);
    }

    private void UpdateSpeed(int collisionCount)
    {
        currentSpeed = Mathf.Max(0, maxSpeed - collisionCount * weight);
    }
}
