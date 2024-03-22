using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRaceBehaviour : MonoBehaviour
{
    private float goalTime = 10;
    private bool won = false;

    private TimerBehaviour timerBehaviour;
    private StartRaceBehaviour startRaceBehaviour;
    private EndRaceBehaviour endRaceBehaviour;

    [SerializeField]
    private GameObject timerManager;

    [SerializeField]
    private GameObject endRaceManager;

    [SerializeField]
    private GameObject raceNPC;

    [SerializeField]
    private GameObject loseText;

    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private Transform startPos;

    [SerializeField]
    private GameObject prize;

    // Start is called before the first frame update
    void Start()
    {
        timerBehaviour = timerManager.GetComponent<TimerBehaviour>();
        startRaceBehaviour = raceNPC.GetComponent<StartRaceBehaviour>();
        endRaceBehaviour = endRaceManager.GetComponent<EndRaceBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerBehaviour.minutes < 1 && timerBehaviour.seconds < goalTime && endRaceBehaviour.raceFinished)
        {
            won = true;
        }
        else
        {
            won = false;
        }

        if(won)
        {
            winText.SetActive(true);
            loseText.SetActive(false);
            prize.SetActive(true);
        }
        else
        {
            winText.SetActive(false);
            loseText.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if(!won && Input.GetKeyDown(KeyCode.E))
            {
                collision.transform.position = startPos.position;
                timerBehaviour.RestartTimer();
                startRaceBehaviour.RestartRace();
            }
        }
    }
}
