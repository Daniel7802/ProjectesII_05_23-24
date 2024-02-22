using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashyIA : Enemy
{
    [SerializeField]
    private AudioClip buzzSound;

    //CHARGING DASH
    [SerializeField]
    float chargingSpeed = 1f;
    [SerializeField]
    float chargingTime = 1f;
    float chargingTimer = 0f;

    //DASH   
    bool lastPlayerPos = false;

    //RELOAD DASH   
    [SerializeField]
    float reloadingTime = 2f;
    float reloadingTimer = 0f;

    TrailRenderer trailRenderer;

    public override void Start()
    {
        base.Start();
        trailRenderer = GetComponent<TrailRenderer>();
        audioSource.clip = buzzSound;
        audioSource.loop = true;
        audioSource.Play();
    }
    void Update()
    {
        
        FlipByTarget();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case CurrentState.ROAMING:
                Roaming();
                break;

            case CurrentState.CHASING:
                Chasing();
                break;

            case CurrentState.RELOADING:
                Reloading();
                break;

        }
    }

    public override void Movement()
    {
        base.Movement();
    }

    public override void Roaming()
    {
        if (distanceToPlayer < startChasingRange && RaycastPlayer())
        {
            StartCoroutine(EnableAlert(targetFoundAlert));
            currentState = CurrentState.CHASING;
        }
        else base.Roaming();
    }

    public override void Chasing()
    {
        if (RaycastPlayer())
        {
            chargingTimer += Time.deltaTime;
            if (chargingTimer < chargingTime)
            {
                
                Vector2 dir = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
                Vector2 chargingForce = dir.normalized * chargingSpeed;
                rb2D.AddForce(chargingForce);
            }
            else
            {                
                
                if (!lastPlayerPos)
                {
                    target = player.transform.position;
                    lastPlayerPos = true;
                }
                moveSpeed = chasingSpeed;                
                Movement();
                trailRenderer.enabled = true;
                if (Vector2.Distance(transform.position,target)<0.5)
                {                    
                    currentState = CurrentState.RELOADING;                   
                    chargingTimer = 0;
                    lastPlayerPos = false;
                }

            }
        }
        else
        {
            StartCoroutine(EnableAlert(lostTargetAlert));
            currentState = CurrentState.ROAMING;
            chargingTimer = 0;
        }
    }

    void Reloading()
    {
        trailRenderer.enabled = false;
        reloadingTimer += Time.deltaTime;
        if (reloadingTimer < reloadingTime)
        {
            Vector2 reloadingForce = transform.up.normalized * roamingSpeed;
            rb2D.AddForce(reloadingForce);
        }
        else
        {
            currentState = CurrentState.ROAMING;
            reloadingTimer = 0;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            currentState = CurrentState.RELOADING;
            chargingTimer = 0;
        }
    }


}
