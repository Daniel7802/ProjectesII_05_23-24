using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDamageSystem : DamageSystem
{
    [SerializeField] private float knockbackForce = 30f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem hs))
        {
            

            if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs) && collision.tag.Equals("Player"))
            {
                if (phs.isInvincible == false && phs.counter < 5)
                {
                    phs.deleteHeart(phs.counter);
                    phs.counter++;
                    phs.turnInvincible();
                    Vector2 dir = new Vector2(phs.transform.position.x - transform.position.x, phs.transform.position.y-transform.position.y);
                    Vector2 kbForce = dir.normalized * knockbackForce;
                    Rigidbody2D rb2D = phs.GetComponent<Rigidbody2D>();
                    rb2D.AddForce(kbForce, ForceMode2D.Impulse);
                    hs.GetDamage(dmg);
                }
            }
        }
    }
}
