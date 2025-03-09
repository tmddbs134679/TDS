using UnityEngine;
using System;

public class SpeedManager : MonoBehaviour
{
    // 충돌 횟수 변경 이벤트
    public event Action<int> OnSpeedChange;

    // 트럭과 몬스터 간 충돌 횟수
    private int collisionCount = 0;

   
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
