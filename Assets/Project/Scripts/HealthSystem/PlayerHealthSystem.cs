using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField]
    GameObject[] hearts;

    public int counter;

    public bool isInvincible = false;
    private float timer = 1f;

    RespawnSystem _respawnSystem;
    AudioSource _audioSource;
    public AudioClip hitSound;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        _respawnSystem = GetComponent<RespawnSystem>();
        _audioSource = GetComponent<AudioSource>();
        counter = 0;
    }

    public void Update()
    {
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
        counter = 0;
        for (int i = 0; i < hearts.Length; i++)
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

    public void deleteHeart(int index)
    {
        
        hearts[index].SetActive(false);
        _audioSource.PlayOneShot(hitSound,0.4f);
    }
    public void addHeart()
    {
        counter--;
        hearts[counter].SetActive(true);
    }
}
