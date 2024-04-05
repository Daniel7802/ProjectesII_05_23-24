using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndActivation : MonoBehaviour
{
    [SerializeField] private GameObject torchManager;  
    [SerializeField] private AudioClip breakSound;
    private AudioSource _audioSource;
    bool soundPlayed = false;
    [SerializeField] private GameObject endCollider;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!soundPlayed)
        {
            if (torchManager == null)
            {
                _audioSource.PlayOneShot(breakSound);
                soundPlayed = true;
                //endCollider.SetActive(true);
            }

        }
         
    }
    
}
