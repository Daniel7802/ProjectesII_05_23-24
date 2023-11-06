using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField]
    public bool torchActive, masterTorch;

    [SerializeField]
    private float time, maxTime;



    private void Start()
    {
        maxTime = 3.0f;
        time = maxTime;
    }
    void Update()
    {
        if(torchActive && !masterTorch)
        {
            TimerFire();
        }
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
