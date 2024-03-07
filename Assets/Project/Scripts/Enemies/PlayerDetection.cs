using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float detectionRadius = 20f;

    private bool playerDetected;
    public Transform playerPos;

    //chasing
    [SerializeField] private float startChasingDistance = 5f;
    [SerializeField] private float stopChasingDistance = 15f;

    public bool chasing = false;

    [SerializeField] private SpriteRenderer foundTargetAlert;
    [SerializeField] private SpriteRenderer lostTargetAlert;
    [SerializeField] private float alertTime = 0.8f;
    private bool found = false;

    [SerializeField] private GameObject roamingPoints;
    protected virtual void Update()
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
            if (found)
            {
                StartCoroutine(EnableAlert(lostTargetAlert));
                found = false;
                roamingPoints.transform.position = transform.position;
            }
        }
    }

    protected IEnumerator EnableAlert(SpriteRenderer sp)
    {
        sp.enabled = true;
        yield return new WaitForSecondsRealtime(alertTime);
        sp.enabled = false;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, startChasingDistance);
        Gizmos.DrawWireSphere(transform.position, stopChasingDistance);
    }
}
