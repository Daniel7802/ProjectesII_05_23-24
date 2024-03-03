using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public bool needsActivator;
    public bool active;
    [SerializeField] private ParticleSystem flameThrowerParticles;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject hotEffect;
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private AudioClip _flameSound;

    bool freezed = false;
    [SerializeField] private float coldTime = 4f;
    private float coldTimer;
    [SerializeField] private GameObject coldEffect;
    [SerializeField] private ParticleSystem iceParticles;
    [SerializeField] private AudioClip _iceSound;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (needsActivator)
        {           
            boxCollider.enabled = false;
            hotEffect.SetActive(false);
            coldEffect.SetActive(false);
            active = false;
        }
        else
        {
            ActiveTrap();
        }
    }

    private void Update()
    {                   
        if(freezed)
        {
            coldTimer += Time.deltaTime;
            if (coldTimer > coldTime)
            {
                ActiveTrap();
                
            }
        }
          
    }
    public void ActiveTrap()
    {
        freezed = false;
        coldEffect.SetActive(false);      
        coldTimer = 0;

        active = true;       

        _audioSource.clip = _flameSound;
        _audioSource.loop = true;
        _audioSource.Play();       
        flameThrowerParticles.Play();
        boxCollider.enabled = true;
        hotEffect.SetActive(true);
        smokeParticles.Play();
    }
    public void Freeze()
    {
        active = false;
        flameThrowerParticles.Stop();
        boxCollider.enabled = false;
        hotEffect.SetActive(false);
        smokeParticles.Stop();
        _audioSource.loop = false;

        freezed = true;
        coldEffect.SetActive(true);
        iceParticles.Play();
        _audioSource.PlayOneShot(_iceSound,20);
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<IceBoomerang>() && active)
        {
            Freeze();
        }
    }
}







