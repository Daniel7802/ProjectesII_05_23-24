using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashyIA : Enemy
{
    //CHARGING DASH
    [SerializeField]
    float chargingSpeed = 3f;
    [SerializeField]
    float chargingTime = 0.7f;
    float chargingTimer = 0f;

    //DASH
    [SerializeField]
    float dashingTime = 1.8f;

    //RELOAD DASH
    [SerializeField]
    float reloadingSpeed = 3f;
    [SerializeField]
    float reloadingTime = 3f;
    float reloadingTimer = 0f;

    TrailRenderer trailRenderer;

    public override void Start()
    {
        base.Start();
        trailRenderer = GetComponent<TrailRenderer>();
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
            StartCoroutine(EnableAlert(detectAlert));
            currentState = CurrentState.CHASING;
        }
        else base.Roaming();
    }

    public override void Chasing()
    {
        target = player.transform.position;

        if (RaycastPlayer())
        {
            chargingTimer += Time.deltaTime;
            if (chargingTimer < chargingTime)
            {
                Vector2 dir = new Vector2(transform.position.x - target.x, transform.position.y - target.y);
                Vector2 chargingForce = dir.normalized * chargingSpeed;
                rb2D.AddForce(chargingForce);
            }
            else
            {
                base.Chasing();
                trailRenderer.enabled = true;
                if (chargingTimer > dashingTime)
                {
                    
                    
                    currentState = CurrentState.RELOADING;
                    chargingTimer = 0;
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
            Vector2 reloadingForce = transform.up.normalized * reloadingSpeed;
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
