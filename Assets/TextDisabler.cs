using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisabler : MonoBehaviour
{
    [SerializeField]
    GameObject timerText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timerText.SetActive(false);
    }
}
