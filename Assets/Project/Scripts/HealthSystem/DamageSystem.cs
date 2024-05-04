using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    protected int dmg;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem hs))
        {
            if (collision.tag != "Player")
            {
                hs.GetDamage(dmg);
            }

            if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs) && collision.tag.Equals("Player") && this.tag != "Boomerang")
            {
                if (!phs.isInvincible && phs.health > 0 && phs.health <= phs.MaxHealth && !phs.isGod)
                {
                    phs.turnInvincible();
                    phs.GetDamage(dmg);
                    phs.deleteHeart();
                    Debug.Log("ESTA PASANDO");
                }
            }
        }

    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (this.tag == "Fire" || this.tag == "Enemy")
        {
            if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs) && collision.tag.Equals("Player") && this.tag != "Boomerang")
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
    public virtual void OnCollisionEnter2D(Collision2D collision) 
    {
        if (this.tag == "Fire" || this.tag == "Enemy")
        {
            if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs) && collision.gameObject.tag.Equals("Player") && this.tag != "Boomerang")
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
    public virtual void OnCollisionStay2D(Collision2D collision)
    {

        if (this.tag == "Fire" || this.tag == "Enemy")
        {
            if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem phs) && collision.gameObject.tag.Equals("Player") && this.tag != "Boomerang")
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
