using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangHUDManager : MonoBehaviour
{
    [SerializeField]
    GameObject boomerang;

    [SerializeField]
    GameObject boomerangImage;

    [SerializeField]
    GameObject shadowBoomerang;

    [SerializeField]
    GameObject shadowBoomerangImage;

    [SerializeField]
    GameObject iceBoomerang;

    [SerializeField]
    GameObject iceBoomerangImage;

    private BoomerangThrow bt;

    // Start is called before the first frame update
    void Start()
    {
        bt = boomerang.GetComponent<BoomerangThrow>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boomerang.activeInHierarchy)
        {
            boomerangImage.SetActive(true);
        }
        else
        {
            boomerangImage.SetActive(false);
        }

        if(shadowBoomerang.activeInHierarchy)
        {
            shadowBoomerangImage.SetActive(true);
        }
        else
        {
            shadowBoomerangImage.SetActive(false);
        }

        if (iceBoomerang.activeInHierarchy)
        {
            iceBoomerangImage.SetActive(true);
        }
        else
        {
            iceBoomerangImage.SetActive(false);
        }
    }
}
