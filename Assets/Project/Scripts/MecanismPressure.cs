using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanismPressure : MonoBehaviour
{
    [SerializeField]
    public bool activate;
    void Start()
    {
        activate = false;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
     if(collision.CompareTag("Player") || collision.CompareTag("Boomerang"))
        {
            activate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Boomerang"))
        {
            activate = false;
        }
    }
}
