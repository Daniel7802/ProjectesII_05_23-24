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

    //shooting
    public GameObject enemyBullet;
    public float bulletPerSecond = 1f;
    float bulletFrequency;
    private bool isShoting = false;
    public float startShootingRange;
    public float stopShootingRange;
    private float shootingTimer = 0;

    private void Start()
    {
        chasingMoveForce = moveForce * chasingForceMultiplier;
        bulletFrequency = 1f / bulletPerSecond;
    }
    private void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < startChasingRange)
        {
            isRoaming = false;
            isChasing = true;
            if (distanceToPlayer < startShootingRange)
            {
                isRoaming = false;
                isChasing = false;
                isShoting = true;

            }
            if (distanceToPlayer > stopShootingRange)
            {

                isShoting = false;
                isRoaming = false;
                isChasing = true;
            }
        }
        else if (distanceToPlayer > stopChasingRange)
        {
            isRoaming = true;
            isChasing = false;
        }

        if (isRoaming)
        {
            Roaming();
        }
        else if (isChasing)
        {
            Chasing();
        }
        else if (isShoting)
        {
            Shooting();
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
        moveForce = chasingMoveForce;
        target = player.transform.position;
        spriteRenderer.color = Color.red;

        Movement();
    }
    void Shooting()
    {
        shootingTimer += Time.deltaTime;
        if (shootingTimer > bulletFrequency)
        {
            ShootOneBullet();
            shootingTimer = 0;
        }
    }
    void ShootOneBullet()
    {
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        GameObject bullet = Instantiate(enemyBullet);
        bullet.transform.position = transform.position;
        bullet.transform.right = dir;
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, startShootingRange);
        Gizmos.DrawWireSphere(transform.position, stopShootingRange);

    }
}
