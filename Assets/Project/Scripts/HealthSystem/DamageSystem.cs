using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    int dmg;
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem hs))
        {
            hs.GetDamage(dmg);
        }
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Vector2 dir = new Vector2(enemy.transform.position.x - transform.position.x,enemy.transform.position.y-transform.position.y);
            enemy.KnockBack(dir);
        }
    }
}
