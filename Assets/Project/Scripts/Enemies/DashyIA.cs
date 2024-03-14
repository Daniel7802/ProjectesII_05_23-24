
using UnityEngine;

public class DashyIA : Enemy
{
    [SerializeField]
    private AudioClip buzzSound;

    //CHARGING DASH
    [SerializeField] float chargingSpeed = 1f;
    [SerializeField] float chargingTime = 1f;
    float chargingTimer = 0f;

    //DASH   
    bool lastPlayerPos = false;
    [SerializeField] float dashingTime = 2f;
    float dashingTimer = 0f;


    //RELOAD DASH   
    [SerializeField] float reloadingTime = 2f;
    float reloadingTimer = 0f;

    TrailRenderer trailRenderer;

    public override void Start()
    {
        base.Start();
        trailRenderer = GetComponent<TrailRenderer>();
        audioSource.clip = buzzSound;
        audioSource.loop = true;
        audioSource.Play();
    }
    void Update()
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

            case CurrentState.RELOADING:
                Reloading();
                break;
        }
    }

    public override void Movement()
    {
        Vector3 dir = target.position - transform.position;
        Vector2 moveForce = dir.normalized * moveSpeed;
        rb2D.AddForce(moveForce, ForceMode2D.Force);
    }

    public override void Roaming()
    {
        if (playerDetection.distanceToPlayer < startChasingDistance && RaycastPlayer())
        {
            StartCoroutine(EnableAlert(foundTargetAlert));
            currentState = CurrentState.CHASING;
        }
        else
        {
            moveSpeed = roamingSpeed;
            Movement();

            if (Vector2.Distance(transform.position, pointA.position) < 0.5f)
            {
                target = pointB;
            }

            if (Vector2.Distance(transform.position, pointB.position) < 0.5f)
            {
                target = pointA;
            }
        }
    }

    public override void Chasing()
    {
        if (playerDetection.distanceToPlayer > stopChasingDistance)
        {
            trailRenderer.enabled = false;
            lastPlayerPos = false;
            chargingTimer = 0;
            StartCoroutine(EnableAlert(lostTargetAlert));
            roamingPoints.transform.position = transform.position;
            target = pointA;
            currentState = CurrentState.ROAMING;

        }
        else
        {
            if (RaycastPlayer())
            {
                chargingTimer += Time.deltaTime;
                if (chargingTimer < chargingTime)
                {

                    Vector2 dir = transform.position - playerDetection.playerTransform.position;
                    Vector2 chargingForce = dir.normalized * chargingSpeed;
                    rb2D.AddForce(chargingForce);
                }
                else
                {
                    dashingTimer += Time.deltaTime;
                    if (!lastPlayerPos)
                    {
                        target = playerDetection.playerTransform;
                        lastPlayerPos = true;
                    }
                    if (dashingTimer < dashingTime)
                    {
                        trailRenderer.enabled = true;
                        moveSpeed = chasingSpeed;
                        Movement();
                    }
                    else
                    {
                        dashingTimer = 0;
                        trailRenderer.enabled = false;
                        lastPlayerPos = false;
                        chargingTimer = 0;
                        currentState = CurrentState.RELOADING;
                    }


                    //if (Vector2.Distance(transform.position, target.position) < 0.5)
                    //{
                    //    trailRenderer.enabled = false;
                    //    lastPlayerPos = false;
                    //    chargingTimer = 0;
                    //    currentState = CurrentState.RELOADING;
                    //}
                }
            }
            else
            {
                trailRenderer.enabled = false;
                lastPlayerPos = false;
                chargingTimer = 0;
                StartCoroutine(EnableAlert(lostTargetAlert));
                roamingPoints.transform.position = transform.position;
                target = pointA;
                currentState = CurrentState.ROAMING;

            }
        }

    }

    void Reloading()
    {
        trailRenderer.enabled = false;
        reloadingTimer += Time.deltaTime;
        if (reloadingTimer < reloadingTime)
        {
            Vector2 reloadingForce = transform.up.normalized * roamingSpeed;
            rb2D.AddForce(reloadingForce);
        }
        else
        {
            reloadingTimer = 0;
            currentState = CurrentState.ROAMING;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            lastPlayerPos = false;
            chargingTimer = 0;
            currentState = CurrentState.RELOADING;
        }
    }


}
