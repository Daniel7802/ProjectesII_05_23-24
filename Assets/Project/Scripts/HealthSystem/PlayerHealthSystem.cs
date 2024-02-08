using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField]
    GameObject[] hearts;
    NewBehaviourScript _damageFlash;
    public int counter;

    public bool isInvincible = false;
    private float timer = 1f;

    RespawnSystem _respawnSystem;
   
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        _respawnSystem = GetComponent<RespawnSystem>();
        _damageFlash = GetComponent<NewBehaviourScript>();
        counter = MaxHealth;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            health = 100;
        }
        if(isInvincible)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                timer = 1f;
                isInvincible = false;
            }
        }
    }

    public void Heal()
    {
        if (health < MaxHealth)
            health++;

        addHeart();
    }

    public void RespawnHeal()
    {
        health = MaxHealth;
        for (int i = MaxHealth - 1; i >= 0; i--)
        {
            hearts[i].SetActive(true);
        }
    }

    public void turnInvincible()
    {
        isInvincible = true;
    }

    public override void DeadState ()
    {       
        _respawnSystem.OnDeath();
    }

    public void deleteHeart()
    {
        _audioSource.PlayOneShot(damageSound);
        _damageFlash.CallDamageFlasher();
        hearts[health - 1].SetActive(false);
    }
    public void addHeart()
    {
        if(health < MaxHealth)
        {
            _damageFlash.CallHealFlasher();
            hearts[health + 1].SetActive(true);
        }
    }
}
