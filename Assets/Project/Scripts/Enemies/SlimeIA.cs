using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeIA : Enemy
{
    public AudioClip slimeJumpSound;

    //movement        
    private float waitingTime;
    private float minWaitingTime;
    private float maxWaitingTime;
    private float waitingTimer = 0;
    private float speedToLand = 1f;

    //roaming    
    private float minWaitingTimeRoaming = 2.5f;
    private float maxWaitingTimeRoaming = 3.5f;

    //chasing      
    private float minWaitingTimeChasing = 0.8f;
    private float maxWaitingTimeChasing = 1.3f;

    public override void Start()
    {
        base.Start();
        SetNewWaitingTime();        
    }

    private void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case CurrentState.ROAMING:
                Roaming();
                break;

            case CurrentState.CHASING:
                Chasing();
                break;
        }

        if (rb2D.velocity.magnitude < speedToLand) animator.SetBool("jump", false);
        else animator.SetBool("jump", true);
    }

    public override void Movement()
    {
        Vector2 dir = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        Vector2 moveForce = dir.normalized * moveSpeed;

        waitingTimer += Time.deltaTime;
        if (waitingTimer >= waitingTime)
        {
            audioSource.PlayOneShot(slimeJumpSound);
            rb2D.AddForce(moveForce, ForceMode2D.Impulse);
            SetNewWaitingTime();
            waitingTimer = 0;
        }
    }

    public override void Roaming()
    {
        if (distanceToPlayer < startChasingRange && RaycastPlayer())
        {
            if (rb2D.velocity.magnitude < speedToLand)
                waitingTimer = waitingTime;
            StartCoroutine(EnableAlert(targetFoundAlert));
            currentState = CurrentState.CHASING;
        }
        else
        {
            minWaitingTime = minWaitingTimeRoaming;
            maxWaitingTime = maxWaitingTimeRoaming;
            base.Roaming();

        }
    }

    public override void Chasing()
    {
        if (distanceToPlayer > stopChasingRange)//target lost --> to roaming
        {
            StartCoroutine(EnableAlert(lostTargetAlert));
            currentState = CurrentState.ROAMING;
        }
        else
        {
            if (RaycastPlayer())
            {
                minWaitingTime = minWaitingTimeChasing;
                maxWaitingTime = maxWaitingTimeChasing;
                base.Chasing();
            }
            else
            {
                StartCoroutine(EnableAlert(lostTargetAlert));
                currentState = CurrentState.ROAMING;
            }
        }


    }

    public void SetNewWaitingTime()
    {
        waitingTime = UnityEngine.Random.Range(minWaitingTime, maxWaitingTime);
    }

}
