using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSystem : MonoBehaviour
{
    public GameObject blood;
    public AudioClip deathSound;
    protected AudioSource _audioSource;
    

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();        
    }
   
    public virtual void Dead()
    {

        Destroy(gameObject);
        Instantiate(blood, transform.position, Quaternion.identity);

    }



}
