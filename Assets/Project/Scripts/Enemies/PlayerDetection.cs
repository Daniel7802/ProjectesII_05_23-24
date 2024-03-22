
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float detectionRadius = 20f;

    protected bool playerDetected;
    public Transform playerTransform;
    public float distanceToPlayer;
    protected virtual void Update()
    {
        // Detecta objetos dentro del radio alrededor del objeto
        Collider2D objectDetected = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (objectDetected != null && objectDetected.CompareTag("Player"))
        {
            playerDetected = true;
            playerTransform = objectDetected.transform;
            distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        }
        else
        {
            playerDetected = false;
        }


    }
}

   
  
