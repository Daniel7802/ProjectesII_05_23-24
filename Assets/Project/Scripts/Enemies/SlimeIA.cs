using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeIA : Enemy
{

    private float jumpTimer = 0;
    private float moveForce;
    public float roamingForce;
    public float jumpForceMultiplier;
    public float waitToJumpSeconds;
    private float chasingJumpForce;

    bool setNewDest = false;
    void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < startChasingRange)
        {
            isRoaming = false;
        }
        else if (distanceToPlayer > stopChasingRange)
        {
            isRoaming = true;
        }

        if (isRoaming)
        {
            Roaming();
        }
        else
        {
            Chasing();
        }


    }
    private void Start()
    {
        chasingJumpForce = moveForce*jumpForceMultiplier;
    }
    public override void Movement()
    {
        Vector2 directionVector = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        Vector2 impulseForce = directionVector.normalized * moveForce;

        jumpTimer += Time.deltaTime;
        if (jumpTimer > waitToJumpSeconds)
        {
            rb2D.AddForce(impulseForce, ForceMode2D.Impulse);
            setNewDest = true;
            jumpTimer = 0;
        }
    }

    public override void Roaming()
    {
        moveForce = roamingForce;
        target = roamingRandomPoint;
        spriteRenderer.color = Color.white;
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
        spriteRenderer.color = Color.red;
        
        Movement();
    }

    

}
