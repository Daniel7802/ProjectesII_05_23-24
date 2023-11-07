using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ShootyIA : Enemy
{

    public float moveForce;

    //roaming
    public float roamingMoveForce;

    private bool isStoped = false;
    private float timerToStop = 0;
    private float timerStoped = 0;
    public float timeToStop;
    public float timeStoped;

    private bool setNewDest = false;
    public float newDestTime;
    private float newDestTimer = 0f;

    //chasing
    public float chasingForceMultiplier;
    private float chasingMoveForce;

    //aiming
    private LineRenderer lineRenderer;
    private float lineTimer = 0f;

    //shooting
    public GameObject enemyBullet;
    private AudioSource source;

    private float shootTimer = 0;
    public float bulletPerSecond = 1f;
    float bulletFrequency;

    public float startShootingRange;
    public float stopShootingRange;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        chasingMoveForce = moveForce * chasingForceMultiplier;
        bulletFrequency = 1f / bulletPerSecond;
    }
    private void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case 0:
                Roaming();
                if (distanceToPlayer < startChasingRange)
                {
                    currentState = 1;
                }
                break;
            case 1:
                Chasing();
                if (distanceToPlayer > stopChasingRange)
                {
                    currentState = 0;
                }
                if (distanceToPlayer < startShootingRange)
                {
                    currentState = 2;
                    //lineTimer = 0f;
                }
                break;

            case 2:
                Aiming();

                break;

            case 3:

                Shooting();
                if (distanceToPlayer < startShootingRange)
                {

                    currentState = 2;

                }
                else
                {
                    currentState = 1;
                }

                break;
        }

    }
    public override void Movement()
    {
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
            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                setNewDest = true;
            }
        }

        timerToStop += Time.deltaTime;
        if (timerToStop > timeToStop)
        {
            isStoped = true;
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
        //lineRenderer.enabled = false;
        moveForce = chasingMoveForce;
        target = player.transform.position;
        spriteRenderer.color = Color.red;

        Movement();
    }
    void Aiming()
    {
        if (lineTimer < 2)
        {
            ShowTrayectoryLine();
            lineTimer += Time.deltaTime;
        }
        else
        {
            lineRenderer.enabled = false;
            currentState = 3;
        }
    }
    void Shooting()
    {
        target = player.transform.position;

        if (shootTimer == 0)
        {
            ShootOneBullet();

        }
        shootTimer += Time.deltaTime;
        if (shootTimer > bulletFrequency)
        {

            shootTimer = 0;
        }
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
