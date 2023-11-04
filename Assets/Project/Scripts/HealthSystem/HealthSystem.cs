using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

   [SerializeField]
   private int MaxHealth, health;
   DeadSystem ds;

    public void Awake()
    {
        ds = GetComponent<DeadSystem>();
    }
    public void Start()
    {
        health = MaxHealth;
    }
    public void GetDamage( int amount)
   {
        health -=amount;

        if(health <= 0)
        {
            health = 0;
            DeadState();
        }
   }

    public void DeadState() 
    {
        ds.Dead();
    }
}