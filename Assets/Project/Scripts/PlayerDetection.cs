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
    private float stopChasingDistance;
    [SerializeField] private float stopChasingRange = 2f;
    public bool chasing = false;


    private void Start()
    {
        stopChasingDistance = startChasingDistance + stopChasingRange;
    }
    void Update()
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
        }
        if (chasing && Vector2.Distance(transform.position, playerPos.transform.position) > stopChasingDistance)
        {
            chasing = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);       
        Gizmos.DrawWireSphere(transform.position, startChasingDistance);
        Gizmos.DrawWireSphere(transform.position, stopChasingDistance);
    }
}
