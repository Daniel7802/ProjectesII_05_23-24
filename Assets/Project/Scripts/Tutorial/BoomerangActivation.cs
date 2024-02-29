using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangActivation : MonoBehaviour
{
    [SerializeField]
    GameObject boomerang;

    //[SerializeField]
    //GameObject boomerangHUD;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boomerang.SetActive(true);
        //boomerangHUD.SetActive(true);
        //Destroy(gameObject);
    }
}
