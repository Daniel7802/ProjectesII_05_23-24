using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEA : MonoBehaviour
{
    [SerializeField] GameObject pl;
    [SerializeField] GameObject eea;
    [SerializeField] Transform tpPos;
    [SerializeField] Transform exitPos;
    public bool ee = false;
    private void Update()
    {
        if (!ee)
        {
            eea.SetActive(false);
        }
        if (ee && Input.GetKeyDown(KeyCode.Escape))
        {
            pl.transform.position = exitPos.position;
            ee = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&Input.GetKeyDown(KeyCode.Comma))
        {
            ee = true;
            eea.SetActive(true);
            pl.transform.position = tpPos.position;
        }
    }
}
