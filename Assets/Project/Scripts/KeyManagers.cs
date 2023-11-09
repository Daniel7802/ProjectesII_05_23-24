using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManagers : MonoBehaviour
{
    [SerializeField]
    public Key[] key;

    [SerializeField]
    public bool activeMechanisme;

    public int counter = 0;

   public virtual void Update()
    {
        for (int i = 0; i < key.Length; i++)
        {
            activeMechanisme = key[i].taken;
        }

        if (activeMechanisme)
        {
            counter++;
            Destroy(gameObject);
        }
    }
}
