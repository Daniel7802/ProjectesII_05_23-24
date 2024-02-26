using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectableSystem : MonoBehaviour
{

    private Rigidbody2D rb2D;  
    public float speed;
    float distanceToPlayer;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            distanceToPlayer = Vector2.Distance(transform.position, collision.transform.position);
            if (distanceToPlayer > 0.5)
            {
                Vector3 dir = collision.transform.position - this.transform.position;

                rb2D.AddForce(dir.normalized * speed);
                speed++;
            }
            else
            {
                Destroy(gameObject);
                if (collision.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs))
                {
                    if (phs.counter > 0)
                        phs.Heal();
                }
            }



        }
    }    

}
