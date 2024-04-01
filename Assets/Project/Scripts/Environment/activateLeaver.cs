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
    List<Cannon> cannons = new List<Cannon>();
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
            activate = true;
            animator.SetBool("active", true);
            if (activateType == type.Trap)
            {
                foreach (Cannon cannon in cannons)
                {
                    cannon.wait = false;
                }
            }
                        
        }
    }

    public void OnDestroy()
    {
        
    }
}
