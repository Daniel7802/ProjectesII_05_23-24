using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    [SerializeField] TorchManager torchManager;

    [SerializeField] Animator animator;

    [SerializeField] Collider2D doorCollider1;

    private void Update()
    {
        if (torchManager.activated)
        {
            animator.SetBool("open", true);            
        }
        if(animator.GetBool("open").Equals(true))
        {
            doorCollider1.isTrigger = true;
        }
        if (animator.GetBool("close").Equals(true))
        {
            doorCollider1.isTrigger = false;
        }  
    }
}
