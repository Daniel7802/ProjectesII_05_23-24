using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    float timeElapsed;

    [SerializeField]
    private GameObject raceNPC;

    private StartRaceBehaviour raceBehaviour;

    private void Start()
    {
        raceBehaviour = raceNPC.GetComponent<StartRaceBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(raceBehaviour.raceStarted)
        {
            timeElapsed += Time.deltaTime;
            float minutes = Mathf.FloorToInt(timeElapsed / 60);
            float seconds = Mathf.FloorToInt(timeElapsed % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
