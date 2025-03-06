using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 2f; // 이동 속도
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
       
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float ownerY = transform.position.y;
        float otherY = collision.transform.position.y;

        if(collision.collider.CompareTag("Monster"))
        {
            if (ownerY != otherY)
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
            }
        }
       
    }

  
}
