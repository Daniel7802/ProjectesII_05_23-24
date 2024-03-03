using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OtherBoomerangActivation : MonoBehaviour
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
        if (collision.CompareTag("Player"))
        {
            if (this.tag.Equals("ShadowBoomerang"))
            {
                bm.m_TargetGroup.RemoveMember(bm.actualBoomerang.transform);
                bm.actualBoomerang.SetActive(false);
                bm.actualBoomerang = bm._shadowBoomerang;
                helpText.SetActive(true);
                bm.shadowBoomerangCollected = true;
                bm.actualBoomerang.SetActive(true);

            }
            else if (this.tag.Equals("IceBoomerang"))
            {
                bm.m_TargetGroup.RemoveMember(bm.actualBoomerang.transform);
                bm.actualBoomerang.SetActive(false);
                bm.actualBoomerang = bm._iceBoomerang;
                helpText.SetActive(true);
                bm.actualBoomerang.SetActive(true);
                bm.iceBoomerangCollected = true;

            }
        }
    }
}
