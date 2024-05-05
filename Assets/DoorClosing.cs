using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosing : MonoBehaviour
{
    [SerializeField] Animator animator;   
    [SerializeField] AudioSource audioSource;   
    [SerializeField] AudioClip closingSound;
    [SerializeField] SpriteRenderer sp;
    bool soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag.Equals("Player"))
        {
            animator.SetBool("close", true);
            sp.sortingOrder = 11;
            if(!soundPlayed )
            {
                audioSource.PlayOneShot(closingSound);
                soundPlayed = true;
            }
        }
    }
}
