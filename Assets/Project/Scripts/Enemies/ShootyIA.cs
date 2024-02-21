using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ShootyIA : Enemy
{

    //roaming

    private bool isStoped = false;
    private float timerToStop = 0f;
    public float timeToStop = 6f;
    private float timerStoped = 0f;
    public float timeStoped = 1f;

    public float newDestTime = 3f;
    private float newDestTimer = 0f;

    //chasing   


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

    private void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        switch (currentState)
        {
            case CurrentState.ROAMING:
                Roaming();
                break;

            case CurrentState.CHASING:
                Chasing();
                break;

            case CurrentState.AIMING:
                Aiming();
                break;

            case CurrentState.SHOOTING:
                Shooting();
                break;

            case CurrentState.RELOADING:
                Reloading();
                break;
        }
    }
    public override void Movement()
    {
        animator.SetBool("walk", true);

        Vector2 directionVector = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        Vector2 impulseForce = directionVector.normalized * moveSpeed;

        rb2D.AddForce(impulseForce, ForceMode2D.Force);
    }

    public override void Roaming()
    {
        if (distanceToPlayer < startChasingRange && RaycastPlayer())
        {
            StartCoroutine(EnableAlert(detectAlert));
            currentState = CurrentState.CHASING;
        }
        else base.Roaming();

    }

    public override void Chasing()
    {
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
            if (RaycastPlayer()) base.Chasing();
            else
            {
                StartCoroutine(EnableAlert(lostTargetAlert));
                currentState = CurrentState.ROAMING;
            }
        }
    }

    void Aiming()
    {
        if (RaycastPlayer())
        {
            if (lineTimer < aimingTime)
            {
                animator.SetBool("walk", false);
                target = player.transform.position;
                ShowTrayectoryLine();
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
            currentState = CurrentState.CHASING;
            lineTimer = 0;
        }

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
        reloadingTimer = 0;
        currentState = CurrentState.RELOADING;
    }
    void ShootOneBullet()
    {
        audioSource.PlayOneShot(shootSound, 0.5f);
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        GameObject bullet = Instantiate(enemyBullet);
        bullet.transform.position = transform.position;
        bullet.transform.right = dir;
    }

    void Reloading()
    {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            setNewDest = true;
        }
    }

}
