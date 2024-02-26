using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public GameObject player;
    public bool playerDetected;
    public float detectionRadius = 5f;
    public LayerMask detectableObjectsLayer;

   
    void Update()
    {

        // Detecta objetos dentro del radio alrededor del objeto
        Collider2D objectDetected = Physics2D.OverlapCircle(transform.position, detectionRadius, detectableObjectsLayer);


        if (objectDetected.CompareTag("Player"))
        {
            player = objectDetected.gameObject;
            playerDetected = true;

        }
        if (Vector2.Distance(transform.position, player.transform.position) > detectionRadius+1)
        {
            playerDetected = false;
        }

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
