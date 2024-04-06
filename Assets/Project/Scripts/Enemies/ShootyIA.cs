
using UnityEngine;


public class ShootyIA : Enemy
{
    //aiming
    private LineRenderer lineRenderer;
    private float lineTimer = 0f;
    [SerializeField] private float aimingTime = 0.8f;
    [SerializeField] private AudioClip foundTargetSound;

    //shooting
    [SerializeField] private GameObject shootyBullet;
    [SerializeField] private float startShootingDistance;
    [SerializeField] private float stopShootingDistance;
    [SerializeField] private AudioClip shootSound;

    //reloading   
    private float reloadingTimer = 0f;
    [SerializeField] private float reloadingTime;
    [SerializeField] private AudioClip reloadingSound;

    [SerializeField] private AudioClip lostTargetSound;
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
                
                //Shooting();
                break;

            case CurrentState.RELOADING:
                Reloading();
                break;
        }

        if (rb2D.velocity.magnitude > 0.5f) animator.SetBool("walk", true);
        else animator.SetBool("walk", false);

    }
    public override void Movement()
    {
        Vector2 dir = target.position - transform.position;
        Vector2 moveForce = dir.normalized * moveSpeed;
        rb2D.AddForce(moveForce, ForceMode2D.Force);
    }
    public override void Roaming()
    {
        if (playerDetection.distanceToPlayer < startChasingDistance && RaycastPlayer())
        {
            audioSource.PlayOneShot(foundTargetSound);
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
            audioSource.PlayOneShot(lostTargetSound);
            StartCoroutine(EnableAlert(lostTargetAlert));
            roamingPoints.transform.position = transform.position;
            target = pointA;
            currentState = CurrentState.ROAMING;
        }
        else if (playerDetection.distanceToPlayer < startShootingDistance && RaycastPlayer())
        {
            currentState = CurrentState.AIMING;
        }
        else
        {
            if (RaycastPlayer())
            {
                target = playerDetection.playerTransform;
                moveSpeed = chasingSpeed;
                Movement();
            }
            else
            {
                audioSource.PlayOneShot(lostTargetSound);
                StartCoroutine(EnableAlert(lostTargetAlert));
                roamingPoints.transform.position = transform.position;
                target = pointA;
                currentState = CurrentState.ROAMING;
            }

        }

    }
    void Aiming()
    {
        if (playerDetection.distanceToPlayer > stopShootingDistance)
        {
            lineRenderer.enabled = false;
            lineTimer = 0f;
           

            currentState = CurrentState.CHASING;
        }
        else
        {
            if (RaycastPlayer())
            {
                if (lineTimer < aimingTime)
                {
                  


                    ShowTrayectoryLine();
                    lineTimer += Time.deltaTime;
                }
                else
                {
                    lineRenderer.enabled = false;
                    lineTimer = 0f;

                    animator.SetBool("Shoot", true);
                    currentState = CurrentState.SHOOTING;


                }
            }
            else
            {
                lineRenderer.enabled = false;
                lineTimer = 0f;
            

                audioSource.PlayOneShot(lostTargetSound);
                StartCoroutine(EnableAlert(lostTargetAlert));
                roamingPoints.transform.position = transform.position;
                target = pointA;
                currentState = CurrentState.ROAMING;
            }
        }

    }
    void ShowTrayectoryLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, Vector3.zero);
            Vector2 position2 = playerDetection.playerTransform.position - transform.position;
            int d = 2;
            lineRenderer.SetPosition(1, new Vector2(position2.x / d, position2.y / d));
        }
    }
    void Shooting()
    {



        target = playerDetection.playerTransform;
        ShootOneBullet();
        animator.SetBool("Shoot", false);

        currentState = CurrentState.RELOADING;

    }
    void ShootOneBullet()
    {
        audioSource.PlayOneShot(shootSound, 0.5f);
        Vector2 dir = playerDetection.playerTransform.position - transform.position;
        GameObject bullet = Instantiate(shootyBullet);
        bullet.transform.position = transform.position;
        bullet.transform.right = dir;
    }
    void Reloading()
    {
        target = playerDetection.playerTransform;

        if (reloadingTimer == 0f)
            audioSource.PlayOneShot(reloadingSound, 0.1f);

        reloadingTimer += Time.deltaTime;

        if (reloadingTimer > reloadingTime)
        {

            if (playerDetection.distanceToPlayer < startShootingDistance && RaycastPlayer())
            {
                currentState = CurrentState.AIMING;
            }
            else if (playerDetection.distanceToPlayer < startChasingDistance && RaycastPlayer())
            {
                currentState = CurrentState.CHASING;
            }
            else
            {
                audioSource.PlayOneShot(lostTargetSound);
                StartCoroutine(EnableAlert(lostTargetAlert));
                roamingPoints.transform.position = transform.position;
                target = pointA;
                currentState = CurrentState.ROAMING;
            }
            reloadingTimer = 0f;



        }
    }

}
