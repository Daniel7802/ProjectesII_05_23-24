using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.UI.Image;
using Unity.VisualScripting;
using static UnityEngine.ParticleSystem;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AudioSource audioSource;

    [SerializeField] protected PlayerDetection playerDetection;


    [SerializeField] protected LayerMask hitLayer;

    public enum CurrentState { ROAMING, CHASING, AIMING, RELOADING, SHOOTING, ICE };
    [SerializeField] public CurrentState currentState = CurrentState.ROAMING;

    public Transform target;
    protected float distanceToPlayer;

    //MOVEMENT
    protected float moveSpeed;

    //ROAMING
    [SerializeField] protected float roamingSpeed;
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;

    //CHASING
    [SerializeField]
    protected float chasingSpeed;

    //hit    
    [SerializeField] GameObject freezeParticles;

    public bool canFreeze = true;


    public virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        target = pointA;
    }

    public virtual void Movement()
    {
        Vector3 dir = target.position - transform.position;
        Vector2 moveForce = dir.normalized * moveSpeed;
        rb2D.AddForce(moveForce, ForceMode2D.Force);
    }

    public virtual void Roaming()
    {
        //if (playerDetection.chasing && RaycastPlayer())
        //{
        //    currentState = CurrentState.CHASING;
        //}
        //else
        //{
        //    moveSpeed = roamingSpeed;
        //    Movement();

        //    if (Vector2.Distance(transform.position, pointA.position) < 0.5f)
        //    {
        //        target = pointB;
        //    }

        //    if (Vector2.Distance(transform.position, pointB.position) < 0.5f)
        //    {
        //        target = pointA;
        //    }
        //}
    }

    public virtual void Chasing()
    {
        if (playerDetection.chasing && RaycastPlayer())
        {
            target = playerDetection.playerPos.transform;
            moveSpeed = chasingSpeed;
            Movement();
        }
        else
        {
            currentState = CurrentState.ROAMING;
            target = pointA;
        }

    }
    public void FlipX()
    {
        if (rb2D.velocity.x > 0) spriteRenderer.flipX = false;
        else if (rb2D.velocity.x == 0)
        {
            if (target.position.x > transform.position.x) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
        }
        else spriteRenderer.flipX = true;
    }


    public void FlipByTarget()
    {
        if (target.position.x > transform.position.x) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }


    protected IEnumerator FreezeRecover()
    {
        canFreeze = false;
        yield return new WaitForSecondsRealtime(6.0f);
        canFreeze = true;
    }
    protected bool RaycastPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerDetection.playerPos.transform.position - transform.position).normalized, 100, hitLayer);
        return hit.rigidbody != null && hit.rigidbody.CompareTag("Player");
    }

    public IEnumerator Ice()
    {
        currentState = CurrentState.ICE;
        StartCoroutine(FreezeRecover());
        GameObject a = Instantiate(freezeParticles, this.transform.position, Quaternion.identity);
        a.transform.SetParent(transform, true);
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.6f, 0.9f, 1);

        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().color = Color.white;

        currentState = CurrentState.ROAMING;
    }

    public virtual void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(pointA.transform.position, 0.2f);
        Gizmos.DrawSphere(pointB.transform.position, 0.2f);
    }

}

