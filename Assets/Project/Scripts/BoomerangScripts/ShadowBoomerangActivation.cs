using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowBoomerangActivation : MonoBehaviour
{
    [SerializeField]
    GameObject boomerangManager;

    private BoomerangManager bm;

    // Start is called before the first frame update
    void Start()
    {
        bm = boomerangManager.GetComponent<BoomerangManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.tag.Equals("ShadowBoomerang"))
        {
            bm.shadowBoomerangCollected = true;
            Destroy(gameObject);
        }
        else if(this.tag.Equals("IceBoomerang"))
        {
            bm.iceBoomerangCollected = true;
            Destroy(gameObject);
        }
    }
}
