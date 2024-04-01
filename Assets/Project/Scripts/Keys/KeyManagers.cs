using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManagers : MonoBehaviour
{
    [SerializeField]
    public Key[] key;
    int get = 0;

    [SerializeField]
    public bool activeMechanisme;

   public virtual void Update()
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (key[i].taken)
            get++;
        }

        if (get == key.Length)
        {
            activeMechanisme = true;
        }
        else
        {
            get = 0;
        }

        if (activeMechanisme)
        {
            Destroy(gameObject);
        }
    }
}
