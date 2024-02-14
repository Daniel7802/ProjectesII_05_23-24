using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateLeaver : MonoBehaviour
{
   public bool activate = false;
    Animator animator;
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
        }
    }
}
