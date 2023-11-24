using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeIA : Enemy
{
    private AudioSource audioSource;
    public AudioClip slimeJumpSound;
    //movement    
    public float waitingTime = 2f;
    private float waitingTimer = 0;
    private float moveForce;
    private float velocityMagnitudeToLand = 1f;
    private float distanceAudio = 8f;

    //roaming
    public float roamingForce;
    bool setNewDest = false;

    //chasing    
    public float chasingJumpForce;

    //damaged
    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }
    private void Start()
    {
        currentState = 0;
        target = roamingRandomPoint;
    }

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

        if ((rb2D.velocity.magnitude < velocityMagnitudeToLand))
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

        waitingTimer += Time.deltaTime;
        if (waitingTimer >= waitingTime)
        {
            if (distanceToPlayer < distanceAudio)
                audioSource.PlayOneShot(slimeJumpSound, 0.5f);
            rb2D.AddForce(impulseForce, ForceMode2D.Impulse);
            setNewDest = true;
            waitingTimer = 0;
        }
    }

    public override void Roaming()
    {
        moveForce = roamingForce;
        target = roamingRandomPoint;
        //spriteRenderer.color = Color.white;
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
        //spriteRenderer.color = Color.red;
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            setNewDest = true;
        }
    }

}
