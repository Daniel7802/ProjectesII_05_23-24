using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class BreachIA : Enemy
{
    //movement
    private float moveForce;

    //roaming
    public float roamingMoveForce = 3f;
    private bool isStoped = false;
    private float timerToStop = 0f;
    public float timeToStop;
    private float timerStoped = 0f;
    public float timeStoped;
    private bool setNewDest = false;
    public float newDestTime;
    private float newDestTimer = 0f;

    //attack
    public GameObject rock;
    public float rockSpeed = 1f;
    public float radius = 5f;
    public float numberOfRocks = 10f;
    public float timeToDestroyRocks = 2f;
    private float waveTimer = 0f;
    private float waveTime = 3.5f;

    public override void Start()
    {
        base.Start();
        currentState = 0;
       
    }


    private void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        switch (currentState)
        {
            case 0:
                if (distanceToPlayer < startChasingRange)
                {

                    currentState = 1;
                }
                else
                {
                    Roaming();
                }
                break;
                
            case 1:
                if (distanceToPlayer > stopChasingRange)
                {
                    currentState = 0;
                }
                else
                {
                    Attack();
                }
                
                break;
        }
    }

    public override void Movement()
    {
        //animator.SetBool("walk", true);

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

    private void Attack()
    {
        float nextAngle = 2 * Mathf.PI / numberOfRocks;
        float angle = 0f;
        waveTimer += Time.deltaTime;
        if(waveTimer > waveTime)
        {
            for (int i = 0; i < numberOfRocks; i++)
            {
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;

                var obj = Instantiate(rock, transform.position, Quaternion.identity);
                var rb = obj.GetComponent<Rigidbody2D>();
                
                rb.velocity = new Vector2(x, y) * rockSpeed;
                angle += nextAngle;

                Destroy(obj,timeToDestroyRocks);
                waveTimer = 0f;
            }

        }
        
        
    }

   
}
