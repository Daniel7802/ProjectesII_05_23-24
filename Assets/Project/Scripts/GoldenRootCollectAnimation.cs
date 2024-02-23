using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoldenRootCollectAnimation : CollectAnimation
{
   
  
    [SerializeField] private AudioClip _idleSound;    

    [SerializeField] GameObject ShopManager;

    private ShopBehaviour sb;

    void Start()
    {
        base.Start();
      
        sb = ShopManager.GetComponent<ShopBehaviour>();
     
    }  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D (collision);  
        if (collision.CompareTag("Player"))
        {
           
            if (!collected)
            {
                sb.currentRoots++;
            }                           
                      
        }
    }
}
