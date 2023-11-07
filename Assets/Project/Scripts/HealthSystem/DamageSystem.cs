using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    int dmg;
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem hs))
        {
            if(collision.tag != "Player")
            {
                hs.GetDamage(dmg);
            }

            if(collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs) && collision.tag.Equals("Player") && this.tag != "Boomerang")
            {
                if(phs.isInvincible == false)
                {
                    phs.deleteHeart(phs.counter);
                    phs.counter++;
                    phs.turnInvincible();
                    hs.GetDamage(dmg);
                }
            }
        }
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Vector2 dir = new Vector2(enemy.transform.position.x - transform.position.x,enemy.transform.position.y-transform.position.y);
            enemy.KnockBack(dir);
        }
    }
}
