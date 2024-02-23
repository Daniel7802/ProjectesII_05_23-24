using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [SerializeField] private ParticleSystem fire;
    private ParticleSystem.EmissionModule emissionModule;
    [SerializeField] private BoxCollider2D boxCollider;
    private void Start()
    {
        emissionModule = fire.emission;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<IceBoomerang>())
        {            
            emissionModule.enabled = false;
            boxCollider.enabled = false;
        }
       
    }
}
