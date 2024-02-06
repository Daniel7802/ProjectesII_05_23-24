using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeIA : Enemy
{
    public LayerMask hitLayer;
    public AudioClip slimeJumpSound;
    //movement    
    private float waitingTime;
    private float minWaitingTime;
    private float maxWaitingTime;
    private float waitingTimer = 0;

    private float minWaitingTimeRoaming = 2.0f;
    private float maxWaitingTimeRoaming = 3.0f;

    private float minWaitingTimeChasing = 1.0f;
    private float maxWaitingTimeChasing = 1.5f;

    private float moveForce;
    private float velocityMagnitudeToLand = 1f;


    //roaming
    public float roamingForce = 8;
    bool setNewDest = false;

    //chasing    
    public float chasingJumpForce = 15;
    [SerializeField]
    SpriteRenderer detectAlert;
    [SerializeField]
    SpriteRenderer lostTargetAlert;



    private void Start()
    {
        base.Start();
        currentState = 0;
        SetNewWaitingTime();
    }

    void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case 0:

                if (distanceToPlayer < startChasingRange && RaycastPlayer())
                {
                    if (waitingTimer > 1.0f)
                        waitingTimer = waitingTime;
                    StartCoroutine(EnableAlert(detectAlert));
                    currentState = 1;
                }
                else
                {
                    Roaming();
                }
                break;
            case 1:

                if (distanceToPlayer > stopChasingRange)//target lost --> to roaming
                {
                    StartCoroutine(EnableAlert(lostTargetAlert));
                    currentState = 0;
                }
                else
                {
                    if (RaycastPlayer())
                        Chasing();
                    else
                    {
                        StartCoroutine(EnableAlert(lostTargetAlert));
                        currentState = 0;
                    }

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
            audioSource.PlayOneShot(slimeJumpSound);
            rb2D.AddForce(impulseForce, ForceMode2D.Impulse);
            SetNewWaitingTime();
            setNewDest = true;
            waitingTimer = 0;
        }
    }

    public override void Roaming()
    {
        moveForce = roamingForce;
        target = roamingRandomPoint;
        minWaitingTime = minWaitingTimeRoaming;
        maxWaitingTime = maxWaitingTimeRoaming;
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
        minWaitingTime = minWaitingTimeChasing;
        maxWaitingTime = maxWaitingTimeChasing;
        Movement();
    }

    public void SetNewWaitingTime()
    {
        waitingTime = UnityEngine.Random.Range(minWaitingTime, maxWaitingTime);
    }

    bool RaycastPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, 100, hitLayer);

        return hit.rigidbody != null && hit.rigidbody.CompareTag("Player");

    }

    private IEnumerator EnableAlert(SpriteRenderer sp)
    {
        sp.enabled = true;
        yield return new WaitForSecondsRealtime(0.8f);
        sp.enabled = false;

    }

    //protected override void OnCollisionEnter2D(Collision2D collision)
    //{
    //    base.OnCollisionEnter2D(collision);

    //    if (collision.gameObject.tag.Equals("Wall"))
    //    {
    //        setNewDest = true;
    //    }
      

    //}

}
