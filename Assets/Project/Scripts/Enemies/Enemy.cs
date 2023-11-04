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
    protected float maxRoamingPointDistance = 2;

    //CHASING    
    public float startChasingRange;

    public float knockbackForce;


    void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < startChasingRange)
        {
            Chasing();
        }
        else
        {
            Roaming();
        }
    }
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
}