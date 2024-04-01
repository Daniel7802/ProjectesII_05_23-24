using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanismPlateBoomerang : MonoBehaviour
{
    [SerializeField]
    public bool activate;

    private bool playerOn;

    void Start()
    {
        activate = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Boomerang"))
        {
            activate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boomerang"))
        {
            if (!playerOn)
            {
                activate = false;
            }
        }
    }
}
