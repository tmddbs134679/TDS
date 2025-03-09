using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public GameObject[] backgrounds;         

    private Vector3 startPos;
    public float maxScrollSpeed = 10f; // 배경 최대 이동 속도
    private float currentScrollSpeed;
    private float backgroundWidth;

    private void Start()
    {
        SpriteRenderer sr = backgrounds[0].GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            backgroundWidth = sr.bounds.size.x;
        }

        startPos = transform.position;  
        currentScrollSpeed = maxScrollSpeed;

        SpeedManager.Instance.OnSpeedChange += UpdateBackgroundSpeed;
    }

    private void OnDestroy()
    {
        SpeedManager.Instance.OnSpeedChange -= UpdateBackgroundSpeed;
    }

    private void Update()
    {
        // 3개의 배경을 왼쪽으로 스크롤
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.Translate(Vector3.left * currentScrollSpeed * Time.deltaTime);
        }

        for (int i = 0; i < backgrounds.Length; i++)
        {
            Transform bg = backgrounds[i].transform;


            if (bg.position.x < -backgroundWidth)
            {
      
                Transform rightMost = GetRightMostBackground();

                float newX = rightMost.position.x + backgroundWidth;
                float newY = bg.position.y;
                float newZ = bg.position.z;

                // 배경 재배치
                bg.position = new Vector3(newX, newY, newZ);
            }
        }
    }


    private void UpdateBackgroundSpeed(int collisionCount)
    {
        currentScrollSpeed = Mathf.Max(0, maxScrollSpeed - collisionCount);
    }


    private Transform GetRightMostBackground()
    {
        Transform rightMost = backgrounds[0].transform;
        for (int i = 1; i < backgrounds.Length; i++)
        {
            if (backgrounds[i].transform.position.x > rightMost.position.x)
            {
                rightMost = backgrounds[i].transform;
            }
        }
        return rightMost;
    }

}
