using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [SerializeField] private ParticleSystem flameThrowerParticles;
    private ParticleSystem.EmissionModule emissionModule;
    [SerializeField] private BoxCollider2D boxCollider;
    private void Start()
    {
        emissionModule = flameThrowerParticles.emission;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<IceBoomerang>())
        {
            Debug.Log("penis");
            emissionModule.enabled = false;
            boxCollider.enabled = false;
        }
       
    }
}
