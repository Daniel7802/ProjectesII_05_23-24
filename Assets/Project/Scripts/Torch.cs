using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Torch : MonoBehaviour
{
   ParticleSystem _particleSystem;
   private ParticleSystem.EmissionModule _missionModule;

    [SerializeField]
    public bool torchActive, masterTorch;

    [SerializeField]
    private float time, maxTime;


    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        _missionModule = _particleSystem.emission;

    }

    private void Start()
    {
        //maxTime = 3.0f;
        time = maxTime;
    }
    void Update()
    {
        if(torchActive && !masterTorch)
        {
            TimerFire();
        }

        if(torchActive)
        {
            _missionModule.enabled = true;
        }
        else
            _missionModule.enabled = false;
    }



    void TimerFire()
    {
        if (time >= 0f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            torchActive = false;
            time = maxTime;
        }
    }
}
