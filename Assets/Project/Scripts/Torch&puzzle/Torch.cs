using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.ParticleSystem;

public class Torch : MonoBehaviour
{
    ParticleSystem _particleSystem;
    private EmissionModule _missionModule;

    [SerializeField]
    public bool isActive, masterTorch, infiniteTime;

    [SerializeField]
    private float time, maxTime;

    [SerializeField]
    Light2D _fireLight;

    public AudioSource _audioSource;
    [SerializeField]
    private AudioClip _torchOnSound;
    [SerializeField]
    private AudioClip _torchOffSound;


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
        if (isActive && !masterTorch)
        {
            TimerFire();
        }

        if (isActive)
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
            isActive = false;
            time = maxTime;
        }
    }

    public void PlayOnSound()
    {
        _audioSource.PlayOneShot(_torchOnSound);
    }
    public void PlayOffSound()
    {
        _audioSource.PlayOneShot(_torchOffSound);

    }


}
