using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class BreachIA : Enemy
{
    //movement
    private float moveForce;

    //roaming
    public float roamingMoveForce = 3f;
    private bool isStoped = false;
    private float timerToStop = 0f;
    public float timeToStop;
    private float timerStoped = 0f;
    public float timeStoped;
    private bool setNewDest = false;
    public float newDestTime;
    private float newDestTimer = 0f;

    //attack

    public ParticleSystem ring;
    ParticleSystem.ShapeModule ringShape;

    public float ringCoolDownTime = 3f;
    private float ringTimer = 0f;



    public override void Start()
    {
        base.Start();
        currentState = 0;
        ringShape = ring.GetComponent<ParticleSystem>().shape;
        ringShape.radius = 0;

    }


    private void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        switch (currentState)
        {
            case 0://roaming
                if (distanceToPlayer < startChasingRange)
                {

                    currentState = 1;
                }
                else
                {
                    Roaming();
                }
                break;

            case 1://attack
                if (distanceToPlayer > stopChasingRange)
                {

                    currentState = 0;
                    ringTimer = 0f;
                    ringShape.radius = 0;
                    ring.Stop();

                }
                else
                {
                    Attack();
                }
                break;
            case 2:
                RingCoolDown();
                break;
        }
    }

    public override void Movement()
    {
        //animator.SetBool("walk", true);

        Vector2 directionVector = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        Vector2 impulseForce = directionVector.normalized * moveForce;

        rb2D.AddForce(impulseForce);
    }

    public override void Roaming()
    {
        moveForce = roamingMoveForce;
        target = roamingRandomPoint;

        if (!isStoped)
        {
            Movement();
            newDestTimer += Time.deltaTime;
            if (newDestTimer > newDestTime)
            {
                setNewDest = true;
                newDestTimer = 0;
            }

            if (Vector2.Distance(transform.position, target) < 1)
            {
                setNewDest = true;
            }
        }

        timerToStop += Time.deltaTime;
        if (timerToStop > timeToStop)
        {
            isStoped = true;

            timerStoped += Time.deltaTime;
            if (timerStoped > timeStoped)
            {
                isStoped = false;
                timerStoped = 0;
                timerToStop = 0;
            }
        }

        if (setNewDest)
        {
            SetNewRoamingDestination();
            setNewDest = false;
        }
       
    }

    private void Attack()
    {
        target = player.transform.position;
        ring.transform.position =new Vector2(transform.position.x,transform.position.y-0.5f);
        ring.Play();

        if (ringShape.radius < 5)
        {
            ringShape.radius += 0.05f;
        }
        else
        {
            ring.Stop();
            currentState = 2;
            ringShape.radius = 0;
        }
    }

    private void RingCoolDown()
    {
        target = player.transform.position;
        if (ringTimer < ringCoolDownTime)
        {
            ringTimer += Time.deltaTime;
        }
        else
        {
            currentState = 1;
            ringTimer = 0;
        }
    }


}
