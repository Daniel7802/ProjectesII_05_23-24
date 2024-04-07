using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool taken;

    [SerializeField]
    private GameObject player;

    private PlayerInventory pI;
    
    void Start()
    {
        taken = false;
        pI = player.GetComponent<PlayerInventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            taken = true;
            pI.IncreaseKeys();
        }
    }
}
