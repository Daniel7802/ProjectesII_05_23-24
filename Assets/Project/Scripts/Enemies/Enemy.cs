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
    protected GameObject player;
    protected Rigidbody2D rb2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AudioSource audioSource;
    protected LayerMask hitLayer;

    protected enum CurrentState { ROAMING, CHASING, AIMING, RELOADING, SHOOTING };
    [SerializeField]
    protected CurrentState currentState = CurrentState.ROAMING;

    protected Vector2 target;
    protected float distanceToPlayer;

    //MOVEMENT
    protected float moveForce;

    //ROAMING
    [SerializeField]
    protected float roamingForce;
    [SerializeField]
    protected CircleCollider2D roamingZone;
    protected Vector2 roamingRandomPoint;
    protected bool setNewDest = false;

    //CHASING    
    public float startChasingRange;
    public float stopChasingRange;

    [SerializeField]
    protected SpriteRenderer detectAlert;
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

    }

    public virtual void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        switch (currentState)
        {

            case CurrentState.ROAMING:
                break;
            case CurrentState.CHASING:
                break;

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

    protected void SetNewRoamingDestination()
    {
        roamingRandomPoint = GetRandomPointInCircle(roamingZone);
    }
    protected Vector2 GetRandomPointInCircle(CircleCollider2D circle)
    {

        Vector2 center = circle.transform.position;

        float randomRadius = Mathf.Sqrt(UnityEngine.Random.value) * circle.radius*this.transform.localScale.x;
        float randomAngle = UnityEngine.Random.Range(0, 2 * Mathf.PI);

        float x = center.x + randomRadius * Mathf.Cos(randomAngle);
        float y = center.y + randomRadius * Mathf.Sin(randomAngle);

        return new Vector2(x, y);
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
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
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
    }
    public virtual void OnDrawGizmos()
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

