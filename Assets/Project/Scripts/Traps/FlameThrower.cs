using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public bool needsActivator;
    bool isActive = false;
    [SerializeField] private ParticleSystem flameThrowerParticles;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject hotEffect;
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private AudioClip _flameSound;

  
    [SerializeField] private float freezedTime = 4f;

    [SerializeField] private GameObject coldEffect;
    [SerializeField] private ParticleSystem iceParticles;
    [SerializeField] private AudioClip _iceSound;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (needsActivator)
        {
            isActive = false;
            boxCollider.enabled = false;
            hotEffect.SetActive(false);
            coldEffect.SetActive(false);
        
        }
        else
        {
            ActivateTrap();
        }
    }
  
    public void ActivateTrap()
    {
        isActive = true;       
               
        flameThrowerParticles.Play();
        boxCollider.enabled = true;
        hotEffect.SetActive(true);
        smokeParticles.Play();

        _audioSource.clip = _flameSound;
        _audioSource.loop = true;
        _audioSource.Play();
    }
    public void DesactivateTrap()
    {
        isActive = false;

        flameThrowerParticles.Stop();
        boxCollider.enabled = false;
        hotEffect.SetActive(false);
        smokeParticles.Stop();
        _audioSource.loop = false;
        _audioSource.Stop();

    }

    private IEnumerator Freezee()
    {
        DesactivateTrap();        
        coldEffect.SetActive(true);
        iceParticles.Play();
        _audioSource.PlayOneShot(_iceSound, 20);

        yield return new WaitForSecondsRealtime(freezedTime);

        coldEffect.SetActive(false);
        ActivateTrap();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<IceBoomerang>()&& isActive)
        {
           StartCoroutine(Freezee());
        }
    }
}







