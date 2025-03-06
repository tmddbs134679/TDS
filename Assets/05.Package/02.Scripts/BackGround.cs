using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public GameObject[] backgrounds;          // 3���� ��� (A, B, C)

    private Vector3 startPos;
    //private float repeatWidth = 20f; 
    public float maxScrollSpeed = 10f; // ��� �ִ� �̵� �ӵ�
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

        // SpeedManager�� OnSpeedChange �̺�Ʈ ����
        SpeedManager.Instance.OnSpeedChange += UpdateBackgroundSpeed;
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ���� ����
        SpeedManager.Instance.OnSpeedChange -= UpdateBackgroundSpeed;
    }

    private void Update()
    {
        // 3���� ����� �������� ��ũ��
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

                // ��� ���ġ
                bg.position = new Vector3(newX, newY, newZ);
            }
        }
    }

    /// <summary>
    /// Ʈ��-���� �浹 Ƚ���� �ٲ� ������ ��� �ӵ��� ������Ʈ
    /// </summary>
    /// <param name="collisionCount">���� Ʈ��-���� �浹 Ƚ��</param>
    private void UpdateBackgroundSpeed(int collisionCount)
    {
        // �浹 Ƚ���� ���� ��� �ӵ��� ���� (����)
        currentScrollSpeed = Mathf.Max(0, maxScrollSpeed - collisionCount * 0.5f);
        Debug.Log($"[Background] Speed updated: {currentScrollSpeed}");
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
