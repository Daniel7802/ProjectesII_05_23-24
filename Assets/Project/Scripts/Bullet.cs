using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    [SerializeField] private Rigidbody2D rb2D;
    
    [SerializeField] float lifeTime = 3f;
    private float lifeTimer = 0f;

    private void FixedUpdate()
    {
        rb2D.velocity = transform.right * speed;
        lifeTimer += Time.deltaTime;
        if(lifeTimer >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Wall"))
        {            
            Destroy(this.gameObject);
        }
    }
}
