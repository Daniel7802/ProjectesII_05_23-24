using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField]
    private Torch[] torch;
    int activated = 0;

    [SerializeField]
    private bool activeMechanisme;
   
    void Update()
    {
        for(int i = 0; i< torch.Length; i++ )
        {
            if(torch[i].torchActive)
                activated++;
        }
        if(activated == torch.Length)
        {
            activeMechanisme = true;
        }
        else
        {
            activated = 0;
        }

        if(activeMechanisme)
        {
            Destroy(gameObject);
        }
    }
}
