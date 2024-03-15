using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    float timeElapsed;

    [SerializeField]
    private GameObject raceNPC;

    private StartRaceBehaviour raceBehaviour;

    public float minutes;
    public float seconds;
    float miliseconds;

    private void Start()
    {
        raceBehaviour = raceNPC.GetComponent<StartRaceBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(raceBehaviour.racing)
        {
            timeElapsed += Time.deltaTime;
            minutes = Mathf.FloorToInt(timeElapsed / 60);
            seconds = Mathf.FloorToInt(timeElapsed % 60);
            //miliseconds = Mathf.FloorToInt(timeElapsed / 1000);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timeElapsed = timeElapsed;
        }
    }

    public void RestartTimer()
    {
        timeElapsed = 0;
        minutes = 0;
        seconds = 0;
    }
}
