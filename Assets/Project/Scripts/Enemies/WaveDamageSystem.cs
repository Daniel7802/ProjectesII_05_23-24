
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDamageSystem : DamageSystem
{
    [SerializeField] private float knockbackForce = 30f;
    public void DamageItem(Collider2D col)
    {
        Debug.Log(col.transform.name);
        if (col.gameObject.TryGetComponent<HealthSystem>(out HealthSystem hs))
        {
            if (col.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs) && col.tag.Equals("Player"))
            {
                if (phs.isInvincible == false)
                {
                  
                    Vector2 dir = new Vector2(phs.transform.position.x - transform.position.x, phs.transform.position.y - transform.position.y);
                    Vector2 kbForce = dir.normalized * knockbackForce;
                    Rigidbody2D rb2D = phs.GetComponent<Rigidbody2D>();
                    rb2D.AddForce(kbForce, ForceMode2D.Impulse);
                    hs.GetDamage(dmg);
                }
            }
        }
    }
}