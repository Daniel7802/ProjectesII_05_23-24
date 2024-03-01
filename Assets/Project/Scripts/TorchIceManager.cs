using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchIceManager : MonoBehaviour
{
    [SerializeField]
    private Torch[] activeTorch;
    [SerializeField]
    private Torch[] nonActiveTorch;
    int activated = 0;
    int nonActivated = 0;

    [SerializeField]
    private bool activeMechanisme;

    void Update()
    {
        for (int i = 0; i < activeTorch.Length; i++)
        {
            if (activeTorch[i].torchActive)
                activated++;
        }       
      
        for (int i = 0; i < nonActiveTorch.Length; i++)
        {
            if (!nonActiveTorch[i].torchActive)
                nonActivated++;
        }
        if (nonActivated == nonActiveTorch.Length && activated == activeTorch.Length)
        {
            activeMechanisme = true;
        }
        else
        {
            activated = 0;
            nonActivated = 0;
        }

        if (activeMechanisme)
        {
            Destroy(gameObject);
        }
    }
}
