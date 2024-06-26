using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameActivator : MonoBehaviour
{
    [SerializeField] private FlameThrower flameThrower;
    private BoxCollider2D ownBoxCollider;
    private void Start()
    {
        ownBoxCollider = GetComponent<BoxCollider2D>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            flameThrower.ActivateTrap();
            ownBoxCollider.enabled = false;
        }

    }
}
