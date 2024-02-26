using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionZone : MonoBehaviour
{
    public GameObject player;
    public bool playerDetected;
    public float detectionRadius = 5f;
    public LayerMask detectableObjectsLayer;

    void Update()
    {
        // Detecta objetos dentro del radio alrededor del objeto
        Collider2D[] objectsDetected = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectableObjectsLayer);

        foreach (var objectDetected in objectsDetected)
        {
            if (objectDetected.tag.Equals("Player"))
            {
                playerDetected = true;
                player = objectDetected.gameObject; 
            }
            //Debug.Log($"{objectDetected.name} detectado dentro del radio");
            // Aquí puedes implementar la lógica específica cuando detectas un objeto
        }
    }

    // Opcional: Dibuja el radio de detección en el editor para visualización
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
