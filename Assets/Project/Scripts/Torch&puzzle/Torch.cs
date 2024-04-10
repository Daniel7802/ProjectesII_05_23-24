using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.ParticleSystem;

public class Torch : MonoBehaviour
{
    ParticleSystem _particleSystem;
    private ParticleSystem.EmissionModule _missionModule;

    [SerializeField]
    public bool torchActive, masterTorch, infiniteTime;

    [SerializeField]
    private float time, maxTime;

    [SerializeField]
    Light2D _fireLight;

    public AudioSource _audioSource;


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
        if (torchActive && !masterTorch)
        {
            TimerFire();
        }

        if (torchActive)
        {
            if (_fireLight.intensity < 1)
            {
                _fireLight.intensity += 0.01f;
            }
            _missionModule.enabled = true;
        }
        else
        {
            if (_fireLight.intensity > 0)
            {
                _fireLight.intensity -= 0.01f;
            }
            _missionModule.enabled = false;
        }
    }



    void TimerFire()
    {
        if (time >= 0f)
        {
            if (!infiniteTime)
            {
                time -= Time.deltaTime;
            }

        }
        else
        {
            torchActive = false;
            time = maxTime;
        }
    }

  
}
