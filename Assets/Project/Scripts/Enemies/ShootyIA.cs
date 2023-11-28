using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ShootyIA : Enemy
{
    
    private float moveForce;

    //roaming
    public float roamingMoveForce = 3f;
    private bool isStoped = false;
    private float timerToStop = 0f;
    public float timeToStop = 6f;
    private float timerStoped = 0f;    
    public float timeStoped = 1f;
    private bool setNewDest = false;
    public float newDestTime = 3f;
    private float newDestTimer = 0f;

    //chasing   
    public float chasingMoveForce = 8f;

    //aiming
    private LineRenderer lineRenderer;
    private float lineTimer = 0f;
    public float aimingTime = 0.8f;

    //shooting
    public GameObject enemyBullet;  
    public float startShootingRange;
    public float stopShootingRange;
    public AudioClip shootSound;

    //reloading   
    private float reloadingTimer = 0f;
    public float reloadingTime;
    public AudioClip reloadingSound;
   
    public override void Start() 
    {
        base.Start();
        currentState = 0;
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
                target = player.transform.position;
                lineTimer = 0;
                if (reloadingTimer == 0f)
                    audioSource.PlayOneShot(reloadingSound, 0.1f);
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
      
        Movement();
    }

    void Aiming()
    {
        target = player.transform.position;
        ShowTrayectoryLine();
    }
    void ShowTrayectoryLine()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.transform.position);
    }

    void Shooting()
    {
        target = player.transform.position;
        ShootOneBullet(); 
    }
    void ShootOneBullet()
    {
        audioSource.PlayOneShot(shootSound,0.5f);
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        GameObject bullet = Instantiate(enemyBullet);
        bullet.transform.position = transform.position;
        bullet.transform.right = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            setNewDest = true;
        }
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, startShootingRange);
        Gizmos.DrawWireSphere(transform.position, stopShootingRange);

    }
}
