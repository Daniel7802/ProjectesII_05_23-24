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
            StartCoroutine(fade1());
        }

        if (ee && Input.GetKeyDown(KeyCode.End))
        {
            StartCoroutine(fade2());
            eea.SetActive(false);
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

    IEnumerator fade1()
    {
        FadeInOutManager.instance.Fadein();
        yield return new WaitForSecondsRealtime(1f);
        pl.transform.position = tpPos.position;
        StartCoroutine(fout());
    }
    IEnumerator fout()
    {

        yield return new WaitForSecondsRealtime(1f);
        FadeInOutManager.instance.Fadeout();
    }
    IEnumerator fade2()
    {
        FadeInOutManager.instance.Fadein();      
        yield return new WaitForSecondsRealtime(1f);
        pl.transform.position = exitPos.position;
        StartCoroutine(fout());

    }
}
