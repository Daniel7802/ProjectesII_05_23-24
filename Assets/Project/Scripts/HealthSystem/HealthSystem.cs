using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField]
    public int MaxHealth;
    public int health;
    protected DeadSystem ds;

    [SerializeField]
    public bool getHit;

    [SerializeField]
    protected AudioSource _audioSource;
    [SerializeField] protected AudioClip damageSound;


    private bool invincible = false;


    private float canGetDamageTimer = 0f;
    private float canGetDamageMaxTimer = 0.5f;
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected Material damagedMaterial;
    protected float damagedTimer = 0.0f;
    [SerializeField] protected float damagedTime = 0.125f;
    protected bool isDamaged;

    public virtual void Awake()
    {
        ds = GetComponent<DeadSystem>();
    }
    public virtual void Start()
    {
        getHit = false;
        canGetDamageTimer = canGetDamageMaxTimer;
        health = MaxHealth;
    }
    public void Update()
    {
        if (isDamaged)
        {
            damagedTimer += Time.deltaTime;
            if (damagedTimer < damagedTime)
            {
                spriteRenderer.material = damagedMaterial;
            }
            else
            {
                damagedTimer = 0.0f;
                isDamaged = false;
            }
        }
        else
        {
            spriteRenderer.material = defaultMaterial;
        }

        if (getHit && !invincible)
        {
            if (canGetDamageTimer >= canGetDamageMaxTimer)
            {
                GetDamage(1);
                canGetDamageTimer = 0.0f;
            }
            canGetDamageTimer += Time.deltaTime;
        }
        else
        {
            canGetDamageTimer = canGetDamageMaxTimer;
        }
    }

    public void GetDamage(int amount)
    {
        if (!ds.isDead)
        {
            _audioSource.PlayOneShot(damageSound);
            health -= amount;

            if (health <= 0)
            {
                health = 0;

                DeadState();
            }

            if (this.gameObject.CompareTag("Enemy"))
            {
                isDamaged = true;
            }
        }
    }
    public void TurnInvencible(bool inv)
    {
        invincible = inv;
    }

    public bool GetInvincible()
    {
        return invincible;
    }

    public virtual void DeadState()
    {
        ds.Dead();
    }
}
