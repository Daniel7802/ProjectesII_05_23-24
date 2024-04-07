
using UnityEngine;



public class SlimeIA : Enemy
{
    public AudioClip slimeJumpSound;

    //movement        
    private float waitingTime;
    private float minWaitingTime;
    private float maxWaitingTime;
    private float waitingTimer = 0;
    private float speedToLand = 1f;

    //roaming    
    private float minWaitingTimeRoaming = 1.5f;
    private float maxWaitingTimeRoaming = 2f;

    //chasing      
    private float minWaitingTimeChasing = 0.8f;
    private float maxWaitingTimeChasing = 1.2f;

    public override void Start()
    {
        base.Start();
        SetNewWaitingTime();
    }

    private void Update()
    {
        FlipX();

        switch (currentState)
        {
            case CurrentState.ROAMING:
                Roaming();
                break;

            case CurrentState.CHASING:
                Chasing();
                break;
            case CurrentState.ICE:
                break;
        }

        if (rb2D.velocity.magnitude < speedToLand) animator.SetBool("jump", false);
        else animator.SetBool("jump", true);
    }

    public override void Movement()
    {
        Vector2 dir = target.position - transform.position;
        Vector2 moveForce = dir.normalized * moveSpeed;

        waitingTimer += Time.deltaTime;
        if (waitingTimer >= waitingTime)
        {
            audioSource.PlayOneShot(slimeJumpSound);
            rb2D.AddForce(moveForce, ForceMode2D.Impulse);
            SetNewWaitingTime();
            waitingTimer = 0;
        }
    }

    public override void Roaming()
    {
        if (playerDetection.distanceToPlayer < startChasingDistance && RaycastPlayer())
        {
            StartCoroutine(EnableAlert(foundTargetAlert));
            waitingTimer = waitingTime;
            currentState = CurrentState.CHASING;
        }
        else
        {
            minWaitingTime = minWaitingTimeRoaming;
            maxWaitingTime = maxWaitingTimeRoaming;
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
            StartCoroutine(EnableAlert(lostTargetAlert));
            roamingPoints.transform.position = transform.position;
            target = pointA;
            currentState = CurrentState.ROAMING;            
        }       
        else
        {
            if (RaycastPlayer())
            {
                minWaitingTime = minWaitingTimeChasing;
                maxWaitingTime = maxWaitingTimeChasing;
                target = playerDetection.playerTransform;
                moveSpeed = chasingSpeed;
                Movement();
            }
            else
            {

                StartCoroutine(EnableAlert(lostTargetAlert));
                roamingPoints.transform.position = transform.position;
                target = pointA;
                currentState = CurrentState.ROAMING;
            }           
        }
    }
    public void SetNewWaitingTime()
    {
        waitingTime = UnityEngine.Random.Range(minWaitingTime, maxWaitingTime);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
