using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRaceBehaviour : MonoBehaviour
{
    private float goalTime = 0;
    private bool won = false;
    private bool canRepeat = false;

    private TimerBehaviour timerBehaviour;
    private StartRaceBehaviour startRaceBehaviour;
    private EndRaceBehaviour endRaceBehaviour;

    [SerializeField]
    private GameObject Player;

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
        if(timerBehaviour.timeElapsed > 0 && endRaceBehaviour.raceFinished)
        {
            won = true;
        }
        else
        {
            won = false;
        }

        if(won && endRaceBehaviour.raceFinished)
        {
            winText.SetActive(true);
            loseText.SetActive(false);
            prize.SetActive(true);
        }
        else if(!won && endRaceBehaviour.raceFinished)
        {
            winText.SetActive(false);
            loseText.SetActive(true);
        }

        if(canRepeat)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Player.transform.position = startPos.position;
                timerBehaviour.RestartTimer();
                startRaceBehaviour.RestartRace();
                endRaceBehaviour.raceFinished = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            canRepeat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canRepeat = false;
    }
}
