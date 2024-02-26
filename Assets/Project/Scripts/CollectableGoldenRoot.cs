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

    public override void Start()
    {
        base.Start();
        sb = ShopManager.GetComponent<ShopBehaviour>();

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {

            if (!collected)
            {
                sb.currentRoots++;
            }

        }
    }
}