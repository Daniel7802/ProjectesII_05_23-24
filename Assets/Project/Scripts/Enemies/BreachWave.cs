using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreachWave : MonoBehaviour    
{
   
    private CircleCollider2D circleCollider2D;
    private ParticleSystem particleSystem1;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float maxRadius = 30f;

    private Rigidbody2D rb2D;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        particleSystem1 = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (particleSystem1)
        {
            circleCollider2D.radius += speed;

        }
        else
        {
            circleCollider2D.radius = 0.001f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
