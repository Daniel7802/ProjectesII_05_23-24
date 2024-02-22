using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateLeaver : MonoBehaviour
{
   public bool activate = false;
    Animator animator;
    enum type { Nothing, Trap };
    [SerializeField]
    type activateType = type.Nothing;
    [SerializeField]
    List<Trap> traps = new List<Trap>();
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
        {
          
            if(collision.GetComponentInParent<BoomerangThrow>().type == BoomerangThrow.boomerangType.NORMAL) 
            {
                activate = true;
                animator.SetBool("active", true);
                if (activateType == type.Trap)
                {
                    foreach (Trap trap in traps)
                    {
                        trap.wait = false;
                    }
                }
            }            
        }
    }
}
