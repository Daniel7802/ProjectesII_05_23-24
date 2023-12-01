using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreachWave : MonoBehaviour    
{
   
    private CircleCollider2D circleCollider2D;
    private ParticleSystem particleSystem1;
    [SerializeField] private float colliderRadiusMultiplier = 6f;
 

    private Rigidbody2D rb2D;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        particleSystem1 = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (particleSystem1.IsAlive() && particleSystem1.particleCount > 0)
            circleCollider2D.radius = particleSystem1.time * colliderRadiusMultiplier;
        else
            circleCollider2D.radius = 0;
        

    }

   
}
