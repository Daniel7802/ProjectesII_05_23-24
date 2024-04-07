using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimeCollectable : MonoBehaviour
{
    [SerializeField]
    GameObject timerManager;

    [SerializeField]
    float timeToAdd;

    [SerializeField]
    SpriteRenderer sr;

    [SerializeField]
    Collider2D collider;
    
    private AudioSource collectSound;

    private TimerBehaviour tb;

    // Start is called before the first frame update
    void Start()
    {
        tb = timerManager.GetComponent<TimerBehaviour>();
        collectSound = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tb.timeElapsed += timeToAdd;
        collectSound.Play();
        sr.enabled = false;
        collider.enabled = false;
    }
}
