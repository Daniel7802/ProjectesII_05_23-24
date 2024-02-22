using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ShootyIA : Enemy
{

    //roaming
    private float waitingTime;
    private float minWaitingTime = 1.5f;
    private float maxWaitingTime = 3f;
    private float waitingTimer = 0;

    //aiming
    private LineRenderer lineRenderer;
    private float lineTimer = 0f;
    [SerializeField]
    private float aimingTime = 0.8f;
    [SerializeField]
    private AudioClip targetFoundSound;

    //shooting
    [SerializeField]
    private GameObject enemyBullet;
    [SerializeField]
    private float startShootingRange;
    [SerializeField]
    private float stopShootingRange;
    [SerializeField]
    private AudioClip shootSound;

    //reloading   
    private float reloadingTimer = 0f;
    [SerializeField]
    private float reloadingTime;
    [SerializeField]
    private AudioClip reloadingSound;

    [SerializeField]
    private AudioClip targetLostSound;

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
        base.Movement();
    }

    public override void Roaming()
    {
        if (distanceToPlayer < startChasingRange && RaycastPlayer())
        {
            audioSource.PlayOneShot(targetFoundSound,0.3f);
            StartCoroutine(EnableAlert(targetFoundAlert));
            currentState = CurrentState.CHASING;
        }
        else
        {
            base.Roaming();
        }

    }

    public override void Chasing()
    {
        if (distanceToPlayer > stopChasingRange)// target lost -->to roaming
        {
            audioSource.PlayOneShot(targetLostSound);
            StartCoroutine(EnableAlert(lostTargetAlert));
            currentState = CurrentState.ROAMING;
        }
        else if (distanceToPlayer < startShootingRange)// player enough close to shoot
        {
            if (RaycastPlayer())
                currentState = CurrentState.AIMING;
            else
            {
                audioSource.PlayOneShot(targetLostSound);
                StartCoroutine(EnableAlert(lostTargetAlert));
                currentState = CurrentState.ROAMING;
            }
        }
        else
        {
            if (RaycastPlayer()) base.Chasing();
            else
            {
                audioSource.PlayOneShot(targetLostSound);
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

    public void SetNewWaitingTime()
    {
        waitingTime = UnityEngine.Random.Range(minWaitingTime, maxWaitingTime);
    }

    IEnumerator RoamingWait()
    {
        yield return new WaitForSecondsRealtime(2);
        setNewDest = true;
    }

}
