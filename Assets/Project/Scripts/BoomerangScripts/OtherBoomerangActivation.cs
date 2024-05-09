using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OtherBoomerangActivation : MonoBehaviour
{
    [SerializeField]
    private BoomerangManager bm;

    [SerializeField] private GameObject helpText;

    // Start is called before the first frame update
    void Start()
    {
        helpText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (this.tag.Equals("ShadowBoomerang"))
            {
                helpText.SetActive(true);
                bm.shadowBoomerangCollected = true;

            }
            else if (this.tag.Equals("IceBoomerang"))
            {
                helpText.SetActive(true);
                bm.iceBoomerangCollected = true;

            }
        }
    }
}
