using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Animator _animator;
    [SerializeField] private float secondsToActivate = 1f;
    [SerializeField] private float secondsToDesactivate = 1f;
    public bool activated = false;

    AudioSource _audioSource;
    [SerializeField] private AudioClip _spikesUp;
    [SerializeField] private AudioClip _spikesDown;
   
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (activated)
        {
            _animator.SetBool("activated", true);
        }
        else
        {
            _animator.SetBool("activated", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ActivateSpikes());
        }
    }  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DesactivateSpikes());
        }
    }

    IEnumerator ActivateSpikes()
    {
        yield return new WaitForSecondsRealtime(secondsToActivate);
       
        if(!activated)
        {
            _audioSource.PlayOneShot(_spikesUp);
            activated = true;
        }       
    }
    IEnumerator DesactivateSpikes()
    {
        yield return new WaitForSecondsRealtime(secondsToDesactivate);
        if(activated)
        {
            _audioSource.PlayOneShot(_spikesDown);
            activated = false;
        }
    }
}
