using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSystem : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb2D;
    protected float distanceToPlayer;
    public float speed;

    public float collectingRange = 3f;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < collectingRange)
        {
            Vector2 directionVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            Vector2 impulseForce = directionVector.normalized * speed;

            rb2D.AddForce(impulseForce);
        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectingRange);
    }
}
