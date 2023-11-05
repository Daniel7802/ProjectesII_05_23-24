using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.UI.Image;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb2D;
    public SpriteRenderer spriteRenderer;
    //public Animator animator;

    protected float distanceToPlayer;

    protected Vector2 target;

    //ROAMING       
    protected Vector2 roamingRandomPoint;
    protected float maxRoamingPointDistance = 5;

    //CHASING    
    public float startChasingRange;
    public float stopChasingRange;

    public float knockbackForce;

    protected bool isRoaming = true;
    protected bool isChasing = false;

  
    public virtual void Movement()
    {

    }

    public virtual void Roaming()
    {
        

    }

    public virtual void Chasing()
    {
        
    }

    public void KnockBack(Vector2 dir)
    {
        Vector2 kbForce = dir.normalized * knockbackForce;
        rb2D.AddForce(kbForce, ForceMode2D.Impulse);
    }

    public void SetNewRoamingDestination()
    {
        roamingRandomPoint = new Vector2(
            UnityEngine.Random.Range(-maxRoamingPointDistance, maxRoamingPointDistance),
            UnityEngine.Random.Range(-maxRoamingPointDistance, maxRoamingPointDistance)
            );
    }

    public void FlipX()
    {
        if (target.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

    }
    public virtual void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, startChasingRange);     
        Gizmos.DrawWireSphere(transform.position, stopChasingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target, 0.4f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(roamingRandomPoint, 0.4f);
    }


}

