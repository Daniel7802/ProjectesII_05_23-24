using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisabler : MonoBehaviour
{
    [SerializeField]
    GameObject startNPC;

    private StartRaceBehaviour raceBehaviour;

    private void Start()
    {
        raceBehaviour = startNPC.GetComponent<StartRaceBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        raceBehaviour.RestartRace();
    }
}
