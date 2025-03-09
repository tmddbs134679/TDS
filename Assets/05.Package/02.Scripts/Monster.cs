using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public enum MonsterState
{
    MoveLeft,
    ClimbUp,
    KnockBack
}

public class Monster : MonoBehaviour, IDamage
{
    public float speed = 2f;  // 이동 속도
    private Rigidbody2D rb;
    public float climbImpulse = 3f;

    private Health health;
    private HealthUI healthUI;
    private GameObject healthUIObj;
    public int damage = 20;


    [SerializeField] private MonsterState state = MonsterState.MoveLeft; // 상태 추적

    private void Awake()
    {
        health = GetComponent<Health>();
        healthUI = GetComponent<HealthUI>();
        healthUIObj = transform.Find("HPPanel").gameObject;
        if (healthUI != null)
        {
            health.HealthChanged += healthUI.UpdateHealthUI;
        }

        rb = GetComponent<Rigidbody2D>();
        UpdateState(); // 초기 상태 적용
    }

    private void OnEnable()
    {
        health.curHealth = 100;
        healthUIObj.SetActive(false);
    }



    private void FixedUpdate()
    {
        if (state == MonsterState.MoveLeft)
        {
            
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    private void UpdateState()
    {
        switch (state)
        {
            case MonsterState.MoveLeft:
                rb.gravityScale = 5;
                rb.mass = 5;
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                break;

            case MonsterState.ClimbUp:
                rb.gravityScale = 1;
                rb.mass = 1;
                rb.velocity = new Vector2(rb.velocity.x, climbImpulse);
                break;

            case MonsterState.KnockBack:
                rb.gravityScale = 1;
                rb.mass = 1;
                rb.velocity = new Vector2(speed  , rb.velocity.y);

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (!healthUIObj.activeSelf)
            {
                healthUIObj.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Box"))
        {
            IDamage damageReciver =  collision.collider.gameObject.GetComponent<IDamage>();
            damageReciver.TakeDamage(damage);
        }

        if (collision.collider.CompareTag("Player"))
        {
            IDamage damageReciver = collision.collider.gameObject.GetComponent<IDamage>();
            damageReciver.TakeDamage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Monster")) return;

        Monster otherMonster = collision.gameObject.GetComponent<Monster>();
        if (otherMonster == null) return;

        float ownerX = transform.position.x;
        float otherX = collision.transform.position.x;
        float ownerY = transform.position.y;
        float otherY = collision.transform.position.y;

        // 뒤에서 오는 몬스터가 같은 높이에 가까울 때
        if (ownerX < otherX && Mathf.Abs(ownerY - otherY) < 0.1f)
        {
            otherMonster.SetState(MonsterState.ClimbUp);
        }
        //  위 몬스터가 아래 몬스터를 누르고 있을 때
        else if (ownerY > otherY && ownerX - 0.1f < otherX && otherMonster.state != MonsterState.KnockBack)
        {
            otherMonster.SetState(MonsterState.KnockBack);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && state != MonsterState.MoveLeft)
        {
            SetState(MonsterState.MoveLeft);
        }
    }

    public void SetState(MonsterState newState)
    {
        if (state == newState) return; 
        state = newState;
        UpdateState();
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }

    private async UniTask ResetKnockBack(float delay)
    {
        await UniTask.Delay((int)(delay * 1000)); 
        SetState(MonsterState.MoveLeft);
    }
}
