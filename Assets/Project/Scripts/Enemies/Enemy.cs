using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.UI.Image;
using Unity.VisualScripting;
using static UnityEngine.ParticleSystem;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    protected Rigidbody2D rb2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AudioSource audioSource;

    [SerializeField]
    protected LayerMask hitLayer;

    protected enum CurrentState { ROAMING, CHASING, AIMING, RELOADING, SHOOTING };
    [SerializeField]
    protected CurrentState currentState = CurrentState.ROAMING;

    protected Vector2 target;
    protected float distanceToPlayer;

    //MOVEMENT
    protected float moveSpeed;

    //ROAMING
    [SerializeField]
    protected float roamingSpeed;
    [SerializeField]
    public CircleCollider2D roamingZone;
    protected Vector2 roamingRandomPoint;
    protected bool setNewDest = false;

    //CHASING    
    [SerializeField]
    protected float chasingSpeed;
    public float startChasingRange;
    public float stopChasingRange;

    [SerializeField]
    protected SpriteRenderer targetFoundAlert;
    [SerializeField]
    protected SpriteRenderer lostTargetAlert;

    //hit
    public GameObject hitParticles;
    public float knockbackForce;


    public virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        SetNewRoamingDestination();
    }

    public virtual void Movement()
    {
        Vector2 dir = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        Vector2 moveForce = dir.normalized * moveSpeed;
        rb2D.AddForce(moveForce, ForceMode2D.Force);

    }

    public virtual void Roaming()
    {
        moveSpeed = roamingSpeed;
        target = roamingRandomPoint;
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

    public virtual void Chasing()
    {
        moveSpeed = chasingSpeed;
        target = player.transform.position;
        Movement();

    }

    public void KnockBack(Vector2 dir)
    {
        Vector2 kbForce = dir.normalized * knockbackForce;
        rb2D.AddForce(kbForce, ForceMode2D.Impulse);
    }

    protected void SetNewRoamingDestination()
    {
        roamingRandomPoint = GetRandomPointInCircle(roamingZone);
    }
    protected Vector2 GetRandomPointInCircle(CircleCollider2D circle)
    {

        Vector2 center = circle.transform.position;

        float randomRadius = Mathf.Sqrt(UnityEngine.Random.value) * circle.radius * this.transform.localScale.x;
        float randomAngle = UnityEngine.Random.Range(0, 2 * Mathf.PI);

        float x = center.x + randomRadius * Mathf.Cos(randomAngle);
        float y = center.y + randomRadius * Mathf.Sin(randomAngle);

        return new Vector2(x, y);
    }
    public void FlipX()
    {
        if (rb2D.velocity.x > 0) spriteRenderer.flipX = false;
        else if (rb2D.velocity.x == 0)
        {
            if (target.x > transform.position.x) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
        }
        else spriteRenderer.flipX = true;
    }
    
    public void FlipByTarget()
    {
        if (target.x > transform.position.x) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }

    protected IEnumerator EnableAlert(SpriteRenderer sp)
    {
        sp.enabled = true;
        yield return new WaitForSecondsRealtime(0.8f);
        sp.enabled = false;

    }
    protected bool RaycastPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, 100, hitLayer);
        return hit.rigidbody != null && hit.rigidbody.CompareTag("Player");
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boomerang"))
        {
            Vector2 dir = transform.position - collision.transform.position;
            KnockBack(dir);

            float angleRadians = Mathf.Atan2(dir.y, dir.x);

            // Convierte el ángulo a grados.
            float angleDegrees = angleRadians * Mathf.Rad2Deg;
            GameObject particles = Instantiate(hitParticles);
            particles.transform.SetParent(transform, true);
            if (GetComponent<LineRenderer>())
                particles.transform.localScale *= 2;

            particles.transform.position = transform.position;
            particles.transform.rotation = Quaternion.Euler(-angleDegrees, 90, -90);

        }
        if (collision.CompareTag("Wall"))
        {
            setNewDest = true;
        }
    }
    public void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, startChasingRange);
        //Gizmos.DrawWireSphere(transform.position, stopChasingRange);
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(target, 0.4f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(roamingRandomPoint, 0.4f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(roamingZone.transform.position, roamingZone.radius * this.transform.localScale.x);
    }

}

