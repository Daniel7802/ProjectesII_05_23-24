using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeIA : Enemy
{
    //movement   
    public float waitToJumpSeconds;
    private float jumpTimer = 0f;
    private float moveForce;
    float velocityMagnitudeToLand = 1f;

    //roaming
    public float roamingForce;
    bool setNewDest = false;

    //chasing    
    public float chasingJumpForce;

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
        
        if ((rb2D.velocity.magnitude<velocityMagnitudeToLand))
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
