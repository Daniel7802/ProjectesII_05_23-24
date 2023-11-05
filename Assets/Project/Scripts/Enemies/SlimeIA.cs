using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlimeIA : Enemy
{

    private float jumpTimer = 0;
    public float jumpForce;
    public float jumpForceMultiplier;
    public float waitToJumpSeconds;
    private float chasingJumpForce;

    bool setNewDest = false;

    private void Start()
    {
        chasingJumpForce = jumpForce*jumpForceMultiplier;
    }
    public override void Movement()
    {
        Vector2 directionVector = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        Vector2 impulseForce = directionVector.normalized * jumpForce;

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
        jumpForce = chasingJumpForce;
        target = player.transform.position;
        spriteRenderer.color = Color.red;
        
        Movement();
    }

}
