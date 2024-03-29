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


    //aiming
    private LineRenderer lineRenderer;
    private float lineTimer = 0f;
    [SerializeField]
    private float aimingTime = 0.8f;
    [SerializeField]
    private AudioClip foundTargetSound;

    //shooting
    [SerializeField]
    private GameObject shootyBullet;
    [SerializeField]
    private float startShootingRange;  
    [SerializeField]
    private AudioClip shootSound;

    //reloading   
    private float reloadingTimer = 0f;
    [SerializeField]
    private float reloadingTime;
    [SerializeField]
    private AudioClip reloadingSound;

    [SerializeField]
    private AudioClip lostTargetSound;

    public override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        FlipByTarget();

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
        if (detectionZone.GetComponent<DetectionZone>().playerDetected && RaycastPlayer())
        {
            audioSource.PlayOneShot(foundTargetSound, 0.3f);
            StartCoroutine(EnableAlert(foundTargetAlert));
            currentState = CurrentState.CHASING;
        }
        else
        {
            base.Roaming();
        }

    }

    public override void Chasing()
    {
        if (detectionZone.GetComponent<DetectionZone>().playerDetected && RaycastPlayer())
        {

            if (Vector2.Distance(transform.position, detectionZone.GetComponent<DetectionZone>().player.transform.position) < startShootingRange)
            {
                currentState = CurrentState.AIMING;
            }
            else
            {
                base.Chasing();
            }

        }
        else
        {
            audioSource.PlayOneShot(lostTargetSound);
            StartCoroutine(EnableAlert(lostTargetAlert));
            currentState = CurrentState.ROAMING;
        }

    }

    void Aiming()
    {

        target = detectionZone.GetComponent<DetectionZone>().player.transform.position;

        if (detectionZone.GetComponent<DetectionZone>().playerDetected && RaycastPlayer())
        {
            if (Vector2.Distance(transform.position, detectionZone.GetComponent<DetectionZone>().player.transform.position) < startShootingRange)
            {
                if (lineTimer < aimingTime)
                {
                    animator.SetBool("walk", false);
                    ShowTrayectoryLine();
                    lineTimer += Time.deltaTime;
                }
                else
                {
                    lineRenderer.enabled = false;
                    lineTimer = 0;
                    currentState = CurrentState.SHOOTING;

                }
            }
            else
            {
                lineRenderer.enabled = false;
                lineTimer = 0f;
                currentState = CurrentState.CHASING;
            }
        }
        else
        {
            lineRenderer.enabled = false;
            lineTimer = 0f;
            audioSource.PlayOneShot(lostTargetSound);
            StartCoroutine(EnableAlert(lostTargetAlert));
            currentState = CurrentState.ROAMING;

        }
    }
    void ShowTrayectoryLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, Vector3.zero);
            Vector3 position2 = detectionZone.GetComponent<DetectionZone>().player.transform.position - transform.position;
            //lineRenderer.SetPosition(1, new Vector3(position2.x / transform.localScale.x, position2.y / transform.localScale.y, position2.z / transform.localScale.z));
            //lineRenderer.SetPosition(1, new Vector3(position2.x, position2.y, position2.z));
            int d = 2;
            lineRenderer.SetPosition(1, new Vector3(position2.x / d, position2.y / d, position2.z / d));

        }
    }

    void Shooting()
    {
        target = detectionZone.GetComponent<DetectionZone>().player.transform.position;
        ShootOneBullet();
        currentState = CurrentState.RELOADING;
    }
    void ShootOneBullet()
    {
        audioSource.PlayOneShot(shootSound, 0.5f);
        Vector2 dir = new Vector2(detectionZone.GetComponent<DetectionZone>().player.transform.position.x - transform.position.x, detectionZone.GetComponent<DetectionZone>().player.transform.position.y - transform.position.y);
        GameObject bullet = Instantiate(shootyBullet);
        bullet.transform.position = transform.position;
        bullet.transform.right = dir;
    }

    void Reloading()
    {
        target = detectionZone.GetComponent<DetectionZone>().player.transform.position;
        if (detectionZone.GetComponent<DetectionZone>().playerDetected && RaycastPlayer())
        {
            if (Vector2.Distance(transform.position, detectionZone.GetComponent<DetectionZone>().player.transform.position) < startShootingRange)
            {
                if (reloadingTimer == 0f)
                    audioSource.PlayOneShot(reloadingSound, 0.1f);

                reloadingTimer += Time.deltaTime;

                if (reloadingTimer > reloadingTime)
                {
                    reloadingTimer = 0f;
                    currentState = CurrentState.AIMING;

                }
               
            }
            else
            {
                reloadingTimer = 0f;
                currentState = CurrentState.CHASING;
            }

        }
        else
        {
            reloadingTimer = 0f;
            audioSource.PlayOneShot(lostTargetSound);
            StartCoroutine(EnableAlert(lostTargetAlert));
            currentState = CurrentState.ROAMING;
        }
        
    }

    public void SetNewWaitingTime()
    {
        waitingTime = UnityEngine.Random.Range(minWaitingTime, maxWaitingTime);
    }

    

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, startShootingRange);
    }

}
