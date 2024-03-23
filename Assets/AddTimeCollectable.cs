using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimeCollectable : MonoBehaviour
{
    [SerializeField]
    GameObject timerManager;

    [SerializeField]
    float timeToAdd;

    private TimerBehaviour tb;

    // Start is called before the first frame update
    void Start()
    {
        tb = timerManager.GetComponent<TimerBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tb.timeElapsed += timeToAdd;
        Destroy(gameObject);
    }
}
