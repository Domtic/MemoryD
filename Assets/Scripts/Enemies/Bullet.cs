using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;


    public void StartMoving(Vector2 shotDirection,float speed)
    {
        rb.velocity = shotDirection * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ///DO SOME DAMAGE
            Destroy(this.gameObject);
        }
        else if(collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }

        
    }
}
