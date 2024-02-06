using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.UI.Image;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    protected Rigidbody2D rb2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AudioSource audioSource;  

    [SerializeField]
    public int currentState = 0;

    protected float distanceToPlayer;

    protected Vector2 target;

    //ROAMING       
    protected Vector2 roamingRandomPoint;
    protected float maxRoamingPointDistance = 4;

    //CHASING    
    public float startChasingRange;
    public float stopChasingRange;

    //hit
    [SerializeField]
    GameObject hitParticles;
    ParticleSystem.ShapeModule hitParticlesShape;
    public float knockbackForce;


    public virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //hitParticlesShape = hitParticles.shape;
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
            UnityEngine.Random.Range(transform.position.x - maxRoamingPointDistance, transform.position.x + maxRoamingPointDistance),
            UnityEngine.Random.Range(transform.position.y - maxRoamingPointDistance, transform.position.y + maxRoamingPointDistance)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            Vector2 dir = transform.position - collision.transform.position;
            KnockBack(dir);
            //double anguloRadianes = Math.Atan2(dir.y, dir.x);            
            //float anguloGrados = (float)(anguloRadianes * (180 / Math.PI));
            //hitParticlesShape.rotation = new Vector3(0,anguloGrados,0);
           
            
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

