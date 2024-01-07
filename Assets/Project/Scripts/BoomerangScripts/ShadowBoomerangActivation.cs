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
        bm.shadowBoomerangCollected = true;
        Destroy(gameObject);
    }
}
