using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool taken;

    private winManagerWithKeys wm;

    [SerializeField]
    GameObject winManager;
    
    void Start()
    {
        taken = false;
        wm = winManager.GetComponent<winManagerWithKeys>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            taken = true;
            
            wm.counter++;
        }
    }
}
