using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyPlayerDetection : PlayerDetection
{
    [SerializeField] float startShootingDistance = 8f;

    public bool shoot = false;

    // Update is called once per frame
    protected  void Update()
    {
        // Detecta objetos dentro del radio alrededor del objeto
        Collider2D objectDetected = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (objectDetected != null && objectDetected.CompareTag("Player"))
        {
            playerPos = objectDetected.transform;
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected && Vector2.Distance(transform.position, playerPos.transform.position) < startChasingDistance)
        {
            chasing = true;
            if (!found)
            {
                StartCoroutine(EnableAlert(foundTargetAlert));
                found = true;
            }
        }

        if (chasing && Vector2.Distance(transform.position, playerPos.transform.position) > stopChasingDistance)
        {
            chasing = false;
            shoot = false;
            if (found)
            {
                StartCoroutine(EnableAlert(lostTargetAlert));
                found = false;
                roamingPoints.transform.position = transform.position;
            }
        }

        if (chasing && Vector2.Distance(transform.position, playerPos.transform.position) < startShootingDistance)
        {            
            shoot = true;
        }
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, startShootingDistance);
    }
}
