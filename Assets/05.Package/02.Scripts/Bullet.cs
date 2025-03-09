using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage = 20f;
    public float lifeTime = 3f;

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            IDamage damageReceiver = collision.GetComponent<IDamage>();
            if (damageReceiver != null)
            {
                damageReceiver.TakeDamage(damage);
            }

            ReturnToPool();
        }
      
    }

    private void ReturnToPool()
    {
        CancelInvoke();
        BulletPool.Instance.ReturnBullet(gameObject); 
    }
}
