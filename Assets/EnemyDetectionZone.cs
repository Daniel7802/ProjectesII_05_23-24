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
            // Aqu� puedes implementar la l�gica espec�fica cuando detectas un objeto
        }
    }

    // Opcional: Dibuja el radio de detecci�n en el editor para visualizaci�n
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
