using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlimeSpawn : MonoBehaviour
{
    private bool invincibleSlimes;
    private float invTimer = 0f;
    [SerializeField]private float invTime = 2f;
   
    void Update()
    {
        invincibleSlimes = GetComponent<HealthSystem>().GetInvincible();
        if (invincibleSlimes)
        {
            if(invTimer<invTime)
            {
                invTimer += Time.deltaTime;
            }
            else
            {
                GetComponent<HealthSystem>().TurnInvencible(false);
                invTime = 0f;
                
            }
            
        }
    }
}
