using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTimer : MonoBehaviour
{
    [SerializeField]
    private GameObject timerManager;

    private TimerBehaviour tb;

    private void Start()
    {
        tb = timerManager.GetComponent<TimerBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            tb.RestartTimer();
        }
    }
}
