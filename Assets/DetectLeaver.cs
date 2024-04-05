using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLeaver : MonoBehaviour
{
    [SerializeField]
    activateLeaver leaver;

    private void Update()
    {
        if(leaver.activate)
        {
            Destroy(gameObject);
        }
    }
}
