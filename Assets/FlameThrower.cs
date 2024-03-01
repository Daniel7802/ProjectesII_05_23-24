using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [SerializeField] private ParticleSystem flameThrowerParticles;
    private ParticleSystem.EmissionModule emissionModule;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject hotEffect;
    [SerializeField] private GameObject smokeParticles;
    [SerializeField] private GameObject coldEffect;
    [SerializeField] private GameObject iceParticles;
    [SerializeField] private float coldTime = 4f;
    private float coldTimer;
    bool freezed = false;
    public bool active ;
    public bool needsActivator;
    private void Start()
    {
        emissionModule = flameThrowerParticles.emission;
        if (needsActivator)
        {
            emissionModule.enabled = false;
            boxCollider.enabled = false;
            hotEffect.SetActive(false);
            coldEffect.SetActive(false);
            iceParticles.SetActive(false);
            smokeParticles.SetActive(false);
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
    public void Freeze()
    {
        active = false;
        freezed = true;
        emissionModule.enabled = false;
        boxCollider.enabled = false;
        hotEffect.SetActive(false);
        smokeParticles.SetActive(false);
        coldEffect.SetActive(true);
        iceParticles.SetActive(true);
        
    }
    public void ActiveTrap()
    {
        freezed = false;
        active = true;
        coldTimer = 0;
        emissionModule.enabled = true;
        boxCollider.enabled = true;
        hotEffect.SetActive(true);
       smokeParticles.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<IceBoomerang>() && active)
        {
            Freeze();
        }
    }
}







