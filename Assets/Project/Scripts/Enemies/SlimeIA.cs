using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlimeIA : Enemy
{

    private float jumpTimer = 0;
    public float jumpForce;
    public float waitToJumpSeconds;
    
    bool setNewDest = false; 


    public override void Movement()
    {
        Vector2 directionVector = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        Vector2 impulseForce = directionVector.normalized * jumpForce;
        
        jumpTimer += Time.deltaTime;
        if (jumpTimer > waitToJumpSeconds)
        {
            rb2D.AddForce(impulseForce,ForceMode2D.Impulse);
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
        target = player.transform.position;
        spriteRenderer.color = Color.red;
        Movement();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, startChasingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target, 0.4f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(roamingRandomPoint, 0.4f);
    }
}
