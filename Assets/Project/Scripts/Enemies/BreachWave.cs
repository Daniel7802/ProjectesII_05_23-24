using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreachWave : MonoBehaviour
{
   
    private ParticleSystem ring;
    ParticleSystem.ShapeModule ringShape;
    public WaveDamageSystem waveDmg;

    private void Start()
    {

        ring = GetComponent<ParticleSystem>();
        ringShape = ring.GetComponent<ParticleSystem>().shape;
    }

    private void FixedUpdate()
    {
        Collider2D[] outColliders = Physics2D.OverlapCircleAll(this.transform.position, ringShape.radius);
        Collider2D[] inColliders = Physics2D.OverlapCircleAll(this.transform.position, ringShape.radius * 0.01f);
        foreach (var collider in outColliders)
        {
            bool found = false;
            foreach (var inCollider in inColliders)
            {
                found |= inCollider == collider;
            }
            if(!found)
                waveDmg.DamageItem(collider);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, ringShape.radius);
    }


}