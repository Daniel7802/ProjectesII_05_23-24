using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRaceBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject raceNPC;

    private StartRaceBehaviour startRace;

    // Start is called before the first frame update
    void Start()
    {
        startRace = raceNPC.GetComponent<StartRaceBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            startRace.racing = false;
        }
    }
}
