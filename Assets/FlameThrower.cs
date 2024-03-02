using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [SerializeField] private ParticleSystem flameThrowerParticles;    
    //private ParticleSystem.EmissionModule flameParticlesEmissionModule;

    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private GameObject hotEffect;
    [SerializeField] private ParticleSystem smokeParticles;
   // private ParticleSystem.EmissionModule smokeParticlesEmissionModule;

    
    [SerializeField] private GameObject coldEffect;
    [SerializeField] private ParticleSystem iceParticles;
   // private ParticleSystem.EmissionModule iceParticlesEmissionModule;
    [SerializeField] private float coldTime = 4f;
    private float coldTimer;
    bool freezed = false;

    public bool active ;
    public bool needsActivator;
    private void Start()
    {
        //flameParticlesEmissionModule = flameThrowerParticles.emission;
        //smokeParticlesEmissionModule = smokeParticles.emission;
        //iceParticlesEmissionModule = iceParticles.emission;

        if (needsActivator)
        {
            //flameParticlesEmissionModule.enabled = false;
            //smokeParticlesEmissionModule.enabled = false;
            //iceParticlesEmissionModule.enabled = false;
           
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
        //iceParticlesEmissionModule.enabled = false;
        coldTimer = 0;

        active = true;
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

        freezed = true;
        coldEffect.SetActive(true);
        iceParticles.Play();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<IceBoomerang>() && active)
        {
            Freeze();
        }
    }
}







