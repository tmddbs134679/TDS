using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float maxSpeed = 200f;   // �ִ� �ӵ�
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = maxSpeed;

        // SpeedManager�� OnSpeedChange �̺�Ʈ ����
        SpeedManager.Instance.OnSpeedChange += UpdateSpeed;
    }

    private void OnDestroy()
    {
        // ������Ʈ�� �ı��� �� �̺�Ʈ ���� ����
        SpeedManager.Instance.OnSpeedChange -= UpdateSpeed;
    }

    private void Update()
    {
        // ���� ȸ��
        transform.Rotate(Vector3.forward, currentSpeed * Time.deltaTime);
    }

    /// <summary>
    /// SpeedManager���� collisionCount�� �ٲ� �� ȣ��Ǵ� �޼���
    /// </summary>
    /// <param name="collisionCount">���� Ʈ��-���� �浹 Ƚ��</param>
    private void UpdateSpeed(int collisionCount)
    {
        // �浹 Ƚ��(collisionCount)�� ���� �ӵ� ����
        // ��: collisionCount * 2f ��ŭ ����
        currentSpeed = Mathf.Max(0, maxSpeed - collisionCount * 2f);
        Debug.Log($"[Wheel] Speed updated: {currentSpeed}");
    }
}
