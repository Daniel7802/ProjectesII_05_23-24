using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowBoomerangActivation : MonoBehaviour
{
    [SerializeField]
    GameObject boomerangManager;

    private BoomerangManager bm;

    [SerializeField] private GameObject helpText;

    // Start is called before the first frame update
    void Start()
    {
        bm = boomerangManager.GetComponent<BoomerangManager>();
        helpText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag.Equals("ShadowBoomerang"))
        {
            
            helpText.SetActive(true);
            bm.shadowBoomerangCollected = true;

        }
        else if (this.tag.Equals("IceBoomerang"))
        {
            bm.iceBoomerangCollected = true;

        }
    }
}
