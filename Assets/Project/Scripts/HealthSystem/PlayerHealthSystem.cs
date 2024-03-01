using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField]
    GameObject[] hearts;

    [SerializeField]
    List<GameObject> heartList = new List<GameObject>();

    NewBehaviourScript _damageFlash;
    public int counter;

    public bool isInvincible = false;
    private float timer = 1f;

    RespawnSystem _respawnSystem;

    private CinemachineImpulseSource impulseSource;
    // Start is called before the first frame update
    public override void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();   
        base.Awake();
        _respawnSystem = GetComponent<RespawnSystem>();
        _damageFlash = GetComponent<NewBehaviourScript>();
        counter = MaxHealth;

        for(int i = 0; i < MaxHealth; i++)
        {
            heartList.Add(hearts[i]);
        }
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
        addHeart();
    }

    public void RespawnHeal()
    {
        health = MaxHealth;
        for (int i = MaxHealth - 1; i >= 0; i--)
        {
            heartList[i].SetActive(true);
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
        heartList[health].SetActive(false);
        cameraShakeManager.instance.CameraShake(impulseSource);
    }
    public void addHeart()
    {
        if(health < MaxHealth)
        {
            _damageFlash.CallHealFlasher();
            heartList[health].SetActive(true);
            health = health + 1;
        }
    }
}
