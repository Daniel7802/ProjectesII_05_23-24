using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreachWave : MonoBehaviour    
{
   
    [SerializeField] private CircleCollider2D outerCircleCollider2D;
    [SerializeField] private CircleCollider2D innerCircleCollider2D;
    private ParticleSystem ring;
    ParticleSystem.ShapeModule ringShape;
    [SerializeField] private float colliderRadiusMultiplier = 6f;
 

    private Rigidbody2D rb2D;

    private void Start()
    {
        
        ring = GetComponent<ParticleSystem>();
        ringShape = ring.GetComponent<ParticleSystem>().shape;
    }

    private void FixedUpdate()
    {       
        outerCircleCollider2D.radius = ringShape.radius;
        innerCircleCollider2D.radius = ringShape.radius - 0.5f;
    }

   
}
