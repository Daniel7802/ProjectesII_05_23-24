using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField]
    protected int MaxHealth, health;
    protected DeadSystem ds;

    [SerializeField]
    private bool getHit;   


    private float canGetDamageTimer = 0.0f;
    [SerializeField] private float canGetDamageMaxTimer = 0.125f;

    private SpriteRenderer spriteRenderer;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material damagedMaterial;
    private float damagedTimer = 0.0f;
    [SerializeField] private float damagedTime = 0.125f;
    private bool isDamaged;

    public virtual void Awake()
    {
        ds = GetComponent<DeadSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        if(getHit)
        {
            if (canGetDamageTimer >= canGetDamageMaxTimer)
            {
                GetDamage(1);
                canGetDamageTimer = 0.0f;
                canGetDamageTimer += Time.deltaTime;
            }
            else
            {
                canGetDamageTimer = canGetDamageMaxTimer;
            }           
        }       
    }

    public void GetDamage(int amount)
    {
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

    public virtual void DeadState()
    {
        ds.Dead();
    }
}
