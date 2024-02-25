using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionZone : MonoBehaviour
{
    public bool playerDetected = false;
    private CircleCollider2D circleCollider;
    public GameObject player;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerDetected = true;            
        }
            

    }
    //public virtual void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        player = collision.gameObject;
    //        playerDetected = true;
    //    }


    //}

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            player = null;
        }

    }   

    
}
