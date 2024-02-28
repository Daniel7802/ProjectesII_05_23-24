using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPlate : MonoBehaviour
{
    [SerializeField]
    private Torch[] _torch;
    int activated = 0;
    bool activeMechanisme = false;

    [SerializeField]
    MecanismPressure _mecanismpressure;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _torch.Length; i++)
        {
            if (_torch[i].torchActive)
                activated++;
        }
        if (activated == _torch.Length)
        {
            activeMechanisme = true;
        }
        else
        {
            activated = 0;
        }

        if (activeMechanisme && _mecanismpressure.activate)
        {
            Destroy(gameObject);
        }
    }
}
