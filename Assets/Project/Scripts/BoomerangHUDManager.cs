using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangHUDManager : MonoBehaviour
{
    [SerializeField]
    GameObject boomerang;

    [SerializeField]
    GameObject boomerangImage;

    private BoomerangThrow bt;

    // Start is called before the first frame update
    void Start()
    {
        bt = boomerang.GetComponent<BoomerangThrow>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!bt.isFlying)
        {
            boomerangImage.SetActive(true);
        }
        else
        {
            boomerangImage.SetActive(false);
        }
    }
}
