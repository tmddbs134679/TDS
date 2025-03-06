using UnityEngine;
using System;

public class SpeedManager : MonoBehaviour
{
    // �浹 Ƚ���� �ٲ� ������ ȣ��Ǵ� �̺�Ʈ
    public event Action<int> OnSpeedChange;

    // Ʈ���� ���� �� �浹 Ƚ��
    private int collisionCount = 0;

    // �̱��� ���� (�ʼ��� �ƴ����� ���Ǹ� ���� ����)
    public static SpeedManager Instance { get; private set; }

    private void Awake()
    { 
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void IncreaseCollisionCount()
    {
        collisionCount++;
        NotifySpeedChange();
    }

    public void DecreaseCollisionCount()
    {
        collisionCount = Mathf.Max(0, collisionCount - 1);
        NotifySpeedChange();
    }

    private void NotifySpeedChange()
    {
        OnSpeedChange?.Invoke(collisionCount);
    }

    public int GetCollisionCount()
    {
        return collisionCount;
    }
}
