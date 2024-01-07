using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneHider : MonoBehaviour
{
    [SerializeField]
    GameObject CentralZone;

    [SerializeField]
    GameObject UnderWorldZone;

    [SerializeField]
    bool isOnCentralZone;

    [SerializeField]
    bool isOnUnderWorldZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if (isOnCentralZone)
            {
                isOnCentralZone = false;
                isOnUnderWorldZone = true;
                UnderWorldZone.SetActive(true);
                CentralZone.SetActive(false);
            }
            
            if(isOnUnderWorldZone)
            {
                isOnCentralZone = true;
                isOnUnderWorldZone = false;
                UnderWorldZone.SetActive(false);
                CentralZone.SetActive(true);
            }
        }
    }
}
