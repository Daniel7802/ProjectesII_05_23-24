using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSystem : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rb2D;
    protected float distanceToPlayer;
    public float speed;
    [SerializeField]

    public float collectingRange = 3f;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);
        if (distanceToPlayer < collectingRange)
        {
            Vector2 directionVector = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            Vector2 impulseForce = directionVector.normalized * speed;

            rb2D.AddForce(impulseForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            if(collision.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs))
            {
                if(phs.counter > 0)
                phs.Heal();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectingRange);
    }

    public void SetTargetPosition(GameObject gm)
    {
        target = gm;
    }

   }
