using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    protected int dmg;

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
                if(phs.isInvincible == false && phs.health > 0 && phs.health <= phs.MaxHealth)
                {
                    phs.turnInvincible();                   
                    phs.GetDamage(dmg);
                    phs.deleteHeart();
                }
            }
        } 
    }
}
