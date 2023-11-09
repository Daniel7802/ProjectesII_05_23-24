using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoomerang : MonoBehaviour
{
    private BoomerangThrow bt;

    [SerializeField]
    int dmg;

    private void Start()
    {
        bt = GetComponent<BoomerangThrow>();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(bt.isFlying)
        {
            if (collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem hs) && !collision.tag.Equals("Player"))
            {
                hs.GetDamage(dmg);

                if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs) && collision.tag.Equals("Player") && this.tag != "Boomerang")
                {
                    phs.deleteHeart(phs.counter);
                    phs.counter++;
                }
            }
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                Vector2 dir = new Vector2(enemy.transform.position.x - transform.position.x, enemy.transform.position.y - transform.position.y);
                enemy.KnockBack(dir);
            }
        }
    }
}