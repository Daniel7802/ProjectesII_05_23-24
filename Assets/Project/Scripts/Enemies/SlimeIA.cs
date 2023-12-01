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

    private float minWaitingTimeRoaming = 2.0f;
    private float maxWaitingTimeRoaming = 3.0f;

    private float minWaitingTimeChasing = 1.0f;
    private float maxWaitingTimeChasing = 1.5f;


    private float moveForce;
    private float velocityMagnitudeToLand = 1f;
    private float distanceAudio = 8f;

    //roaming
    public float roamingForce = 8;
    bool setNewDest = false;

    //chasing    
    public float chasingJumpForce = 15;

    private void Start()
    {
        base.Start();
        currentState = 0;      
        SetNewWaitingTime();
    }

    void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case 0:

                if (distanceToPlayer < startChasingRange)
                {

                    currentState = 1;
                }
                else
                {
                    Roaming();
                }
                break;
            case 1:

                if (distanceToPlayer > stopChasingRange)
                {
                    currentState = 0;
                }
                else
                {
                    Chasing();
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

            if (distanceToPlayer < distanceAudio)
                audioSource.PlayOneShot(slimeJumpSound, 0.5f);

            rb2D.AddForce(impulseForce, ForceMode2D.Impulse);
            SetNewWaitingTime();
            setNewDest = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            setNewDest = true;
        }
    }

}
