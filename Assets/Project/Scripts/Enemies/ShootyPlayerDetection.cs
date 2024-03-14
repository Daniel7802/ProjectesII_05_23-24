using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyPlayerDetection : PlayerDetection
{
    [SerializeField] float startShootingDistance = 8f;

    public bool shoot = false;


    protected void Update()
    {
        // Detecta objetos dentro del radio alrededor del objeto
        Collider2D objectDetected = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (objectDetected != null && objectDetected.CompareTag("Player"))
        {
            playerTransform = objectDetected.transform;
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected && Vector2.Distance(transform.position, playerTransform.transform.position) < startChasingDistance)
        {
            chasing = true;
            if (!found)
            {
                StartCoroutine(EnableAlert(foundTargetAlert));
                found = true;
            }
        }

        if (chasing && Vector2.Distance(transform.position, playerTransform.transform.position) > stopChasingDistance)
        {
            chasing = false;

            if (found)
            {
                StartCoroutine(EnableAlert(lostTargetAlert));
                found = false;
                roamingPoints.transform.position = transform.position;
            }
        }

        if (Vector2.Distance(transform.position, playerTransform.transform.position) < startShootingDistance)
        {
            
            shoot = true;
        }
        else
        {
            shoot = false;
        }
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, startShootingDistance);
    }
}
