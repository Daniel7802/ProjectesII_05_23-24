using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EE : MonoBehaviour
{
    [SerializeField] GameObject pl;
    [SerializeField] GameObject eea;
    [SerializeField] Transform tpPos;
    [SerializeField] Transform exitPos;
    public bool ee = false;
    bool onTrigger = false;
    private void Update()
    {
        if (onTrigger && Input.GetKeyDown(KeyCode.Comma))
        {
            ee = true;
            eea.SetActive(true);
            pl.transform.position = tpPos.position;
        }
        if (!ee)
        {
            eea.SetActive(false);
        }
        if (ee && Input.GetKeyDown(KeyCode.End))
        {
            pl.transform.position = exitPos.position;
            ee = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger = false;
        }
    }
}
