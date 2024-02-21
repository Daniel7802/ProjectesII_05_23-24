using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ShootyIA : Enemy
{

    //roaming
    public float roamingMoveForce = 3f;
    private bool isStoped = false;
    private float timerToStop = 0f;
    public float timeToStop = 6f;
    private float timerStoped = 0f;
    public float timeStoped = 1f;
    
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
        lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Update()
    {
        base.Update();

        switch (currentState)
        {
            case CurrentState.ROAMING:

                if (distanceToPlayer < startChasingRange && RaycastPlayer())
                {
                    StartCoroutine(EnableAlert(detectAlert));
                    currentState = CurrentState.CHASING;
                }
                else
                {
                    Roaming();
                }
                break;

            case CurrentState.CHASING:

                if (distanceToPlayer > stopChasingRange)// target lost -->to roaming
                {
                    StartCoroutine(EnableAlert(lostTargetAlert));
                    currentState = CurrentState.ROAMING;
                }
                else if (distanceToPlayer < startShootingRange)// player enough close to shoot
                {
                    if (RaycastPlayer())
                        currentState = CurrentState.AIMING;
                    else
                    {
                        StartCoroutine(EnableAlert(lostTargetAlert));
                        currentState = CurrentState.ROAMING;
                    }

                }
                else
                {
                    if (RaycastPlayer())
                        Chasing();
                    else
                    {
                        StartCoroutine(EnableAlert(lostTargetAlert));
                        currentState = CurrentState.ROAMING;
                    }
                }
                break;

            case CurrentState.AIMING:
                if (RaycastPlayer())
                {
                    if (lineTimer < aimingTime)
                    {
                        animator.SetBool("walk", false);
                        Aiming();
                        lineTimer += Time.deltaTime;
                    }
                    else
                    {
                        lineRenderer.enabled = false;
                        currentState = CurrentState.SHOOTING;
                    }
                }
                else
                {
                    lineRenderer.enabled = false;
                    currentState = CurrentState.SHOOTING;
                    lineTimer = 0;
                }

                break;

            case CurrentState.SHOOTING:
                Shooting();
                reloadingTimer = 0;
                currentState = CurrentState.RELOADING;
                break;

            case CurrentState.RELOADING:
                target = player.transform.position;
                lineTimer = 0;
                if (reloadingTimer == 0f)
                    audioSource.PlayOneShot(reloadingSound, 0.1f);
                reloadingTimer += Time.deltaTime;

                if (reloadingTimer > reloadingTime)
                {
                    if (distanceToPlayer < startShootingRange)
                    {
                        currentState = CurrentState.AIMING;
                    }
                    else
                    {
                        currentState = CurrentState.CHASING;
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

            if (Vector2.Distance(transform.position, target) < 1)
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
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, Vector3.zero);
            Vector3 position2 = player.transform.position - transform.position;
            lineRenderer.SetPosition(1, new Vector3(position2.x / transform.localScale.x, position2.y / transform.localScale.y, position2.z / transform.localScale.z));
            //lineRenderer.SetPosition(0, transform.position);
            // lineRenderer.SetPosition(1, player.transform.position);

        }


    }

    void Shooting()
    {
        target = player.transform.position;
        ShootOneBullet();
    }
    void ShootOneBullet()
    {
        audioSource.PlayOneShot(shootSound, 0.5f);
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
