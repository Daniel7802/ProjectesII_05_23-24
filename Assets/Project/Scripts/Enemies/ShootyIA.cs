using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ShootyIA : Enemy
{

    private float moveForce;

    //roaming
    public float roamingMoveForce;
    private bool isStoped = false;
    private float timerToStop = 0f;
    private float timerStoped = 0f;
    public float timeToStop;
    public float timeStoped;

    private bool setNewDest = false;
    public float newDestTime;
    private float newDestTimer = 0f;

    //chasing   
    public float chasingMoveForce;

    //aiming
    private LineRenderer lineRenderer;
    private float lineTimer = 0f;
    public float aimingTime;

    //shooting
    public GameObject enemyBullet;
    private AudioSource source;
    public float startShootingRange;
    public float stopShootingRange;
    
    //reloading
    private float reloadingTimer = 0f;
    public float reloadingTime;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    private void Update()
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
                else if (distanceToPlayer < startShootingRange)
                {
                    currentState = 2;
                }
                else
                {
                    Chasing();
                }
                break;

            case 2:
                if (lineTimer < aimingTime)
                {
                    animator.SetBool("walk", false);
                    Aiming();                    
                    lineTimer += Time.deltaTime;
                }
                else
                {
                    lineRenderer.enabled = false;
                    currentState = 3;
                }
                break;

            case 3:                
                Shooting();
                reloadingTimer = 0;
                currentState = 4;
                break;

            case 4:
                lineTimer = 0;
                reloadingTimer += Time.deltaTime;
                if (reloadingTimer > reloadingTime)
                {
                    if (distanceToPlayer < startShootingRange)
                    {                        
                        currentState = 2;
                    }
                    else
                    {
                        currentState = 1;
                    }
                }
                break;

        }

    }
    public override void Movement()
    {
        animator.SetBool("walk", true);
        
        Vector2 directionVector = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        Vector2 impulseForce = directionVector.normalized * moveForce;

        rb2D.AddForce(impulseForce);
    }

    public override void Roaming()
    {
        moveForce = roamingMoveForce;
        target = roamingRandomPoint;
        spriteRenderer.color = Color.white;
        if (!isStoped)
        {
            Movement();
            newDestTimer += Time.deltaTime;
            if (newDestTimer > newDestTime)
            {
                setNewDest = true;
                newDestTimer = 0;
            }
            
            if (Vector2.Distance(transform.position,target)<1)
            {
                setNewDest = true;
            }
        }

        timerToStop += Time.deltaTime;
        if (timerToStop > timeToStop)
        {
            isStoped = true;
            animator.SetBool("walk", false);
            timerStoped += Time.deltaTime;
            if (timerStoped > timeStoped)
            {
                isStoped = false;
                timerStoped = 0;
                timerToStop = 0;
            }
        }

        if (setNewDest)
        {
            SetNewRoamingDestination();
            setNewDest = false;
        }
    }

    public override void Chasing()
    {
        moveForce = chasingMoveForce;
        target = player.transform.position;
        spriteRenderer.color = Color.red;
        Movement();
    }
    void Aiming()
    {
        target = player.transform.position;
        ShowTrayectoryLine();
    }
    void Shooting()
    {
        target = player.transform.position;
        ShootOneBullet(); 
    }
    void ShootOneBullet()
    {
        source.PlayOneShot(source.clip);
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        GameObject bullet = Instantiate(enemyBullet);
        bullet.transform.position = transform.position;
        bullet.transform.right = dir;
    }

    void ShowTrayectoryLine()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.transform.position);
      

    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, startShootingRange);
        Gizmos.DrawWireSphere(transform.position, stopShootingRange);

    }
}
