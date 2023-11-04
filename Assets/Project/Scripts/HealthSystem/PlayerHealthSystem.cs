using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    RespawnSystem _respawnSystem;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        _respawnSystem = GetComponent<RespawnSystem>(); 
    }
    public void Heal()
    {

    }

    public void RespawnHeal()
    {
        health = MaxHealth;
    }

    public override void DeadState ()
    {
        _respawnSystem.OnDeath();
    }
}
