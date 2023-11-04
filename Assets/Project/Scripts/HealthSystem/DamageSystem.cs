using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    int dmg;
     void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem hs))
        {
            hs.GetDamage(dmg);
        }
    }
}
