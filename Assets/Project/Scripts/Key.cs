using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool taken;

    private winManagerWithKeys wm;

    [SerializeField]
    GameObject winManager;

    Vector3 position = new Vector3(-13, 4, 0);
    void Start()
    {
        taken = false;
        wm = winManager.GetComponent<winManagerWithKeys>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            taken = true;
            collision.gameObject.transform.position = position;
            wm.counter++;
        }
    }
}
