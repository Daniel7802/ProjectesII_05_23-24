using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashyIA : Enemy
{
    [SerializeField]
    float chargingSpeed = 3f;
    [SerializeField]
    float chargingTime = 1f;
    float chargingTimer = 0f;

    public override void Start()
    {
        base.Start();

    }


    void Update()
    {
        FlipByTarget();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case CurrentState.ROAMING:
                Roaming();
                break;

            case CurrentState.CHASING:
                Chasing();
                break;

        }
    }

    public override void Movement()
    {

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
        target = player.transform.position;

        if (RaycastPlayer())
        {

            if (chargingTimer < chargingTime)
            {
                chargingTimer += Time.deltaTime;
                Vector2 dir = new Vector2(transform.position.x - target.x, transform.position.y - target.y);
                Vector2 chargingForce = dir.normalized * chargingSpeed;
                rb2D.AddForce(chargingForce);

            }
            else
            {
                base.Chasing();
                if (chargingTimer > chargingTime * 3)
                    chargingTimer = 0;
            }


        }
        else
        {
            StartCoroutine(EnableAlert(lostTargetAlert));
            currentState = CurrentState.ROAMING;
        }
    }

    //IEnumerator ChargingDash()
    //{

    //    //yield return new WaitForSecondsRealtime(chargingTime);


    //}
}
