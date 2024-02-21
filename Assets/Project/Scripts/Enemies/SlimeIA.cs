using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeIA : Enemy
{
    public AudioClip slimeJumpSound;

    //movement    
    private float velocityMagnitudeToLand = 1f;
    private float waitingTime;
    private float minWaitingTime;
    private float maxWaitingTime;
    private float waitingTimer = 0;

    //roaming    
    private float minWaitingTimeRoaming = 2.5f;
    private float maxWaitingTimeRoaming = 3.5f;    

    //chasing    
    [SerializeField]
    private float chasingJumpForce = 15f;
    private float minWaitingTimeChasing = 0.8f;
    private float maxWaitingTimeChasing = 1.3f;

    public override void Start()
    {
        base.Start();
        SetNewRoamingDestination();
        SetNewWaitingTime();
    }

    public override void Update()
    {
        base.Update();

        switch (currentState)
        {
            case CurrentState.ROAMING:

                if (distanceToPlayer < startChasingRange && RaycastPlayer())
                {
                    if (waitingTimer > 1.0f)
                        waitingTimer = waitingTime;
                    StartCoroutine(EnableAlert(detectAlert));
                    currentState = CurrentState.CHASING;
                }
                else
                {
                    Roaming();
                }
                break;
            case CurrentState.CHASING:

                if (distanceToPlayer > stopChasingRange)//target lost --> to roaming
                {
                    StartCoroutine(EnableAlert(lostTargetAlert));
                    currentState = CurrentState.ROAMING;
                }
                else
                {
                    if (RaycastPlayer())
                        Chasing();
                    else
                    {
                        StartCoroutine(EnableAlert(lostTargetAlert));
                        currentState = CurrentState.ROAMING;
                    }

                }
                break;

        }

        if ((rb2D.velocity.magnitude < velocityMagnitudeToLand))
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }

    }

    public override void Movement()
    {
        Vector2 directionVector = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        Vector2 impulseForce = directionVector.normalized * moveForce;

        waitingTimer += Time.deltaTime;
        if (waitingTimer >= waitingTime)
        {
            audioSource.PlayOneShot(slimeJumpSound);
            rb2D.AddForce(impulseForce, ForceMode2D.Impulse);
            SetNewWaitingTime();
            waitingTimer = 0;
        }
    }

    public override void Roaming()
    {
        moveForce = roamingForce;
        target = roamingRandomPoint;
        minWaitingTime = minWaitingTimeRoaming;
        maxWaitingTime = maxWaitingTimeRoaming;
        Movement();
        if (Vector2.Distance(transform.position, target) < 0.2)
        {
            setNewDest = true;
        }
        if (setNewDest)
        {
            SetNewRoamingDestination();
            setNewDest = false;
        }
    }

    public override void Chasing()
    {

        moveForce = chasingJumpForce;
        target = player.transform.position;
        minWaitingTime = minWaitingTimeChasing;
        maxWaitingTime = maxWaitingTimeChasing;
        Movement();
    }

    public void SetNewWaitingTime()
    {
        waitingTime = UnityEngine.Random.Range(minWaitingTime, maxWaitingTime);
    }

}
