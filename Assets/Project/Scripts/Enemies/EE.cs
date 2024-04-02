using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EE : MonoBehaviour
{

    private AudioSource _audioSource;
    public AudioClip ee;
    bool isin = false;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

  

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
           
            if (!isin)
            {
                _audioSource.Play();
                isin = true;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _audioSource.Stop();    
        isin = false;
    }
}
