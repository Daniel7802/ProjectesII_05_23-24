
using UnityEngine;

public class SpikesDamageSystem : DamageSystem
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //if (this.GetComponent<Spikes>().activated)
        //{
        //    if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs))
        //    {
        //        if (phs.isInvincible == false && phs.health > 0 && phs.health <= phs.MaxHealth)
        //        {

        //            phs.turnInvincible();
        //            phs.GetDamage(dmg);
        //            phs.deleteHeart();
        //        }
        //    }
        //}

    }
    public override void OnTriggerStay2D(Collider2D collision)
    { 

        if (this.GetComponent<Spikes>().activated)
        {
            if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs))
            {
                if (phs.isInvincible == false && phs.health > 0 && phs.health <= phs.MaxHealth)
                {

                    phs.turnInvincible();
                    phs.GetDamage(dmg);
                    phs.deleteHeart();
                }
            }
        }
    }
}
