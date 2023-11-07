using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    [SerializeField]
    private Torch[] torch;

    [SerializeField]
    private bool activeMechanisme;
   
    void Update()
    {
        for(int i = 0; i< torch.Length; i++ )
        {
            activeMechanisme = torch[i].torchActive;
        }

        if(activeMechanisme)
        {
            Destroy(gameObject);
        }
    }
}
