using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    public float timeElapsed = 45f;

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
        if(raceBehaviour.racing)
        {
            timeElapsed -= Time.deltaTime;
            timerText.text = timeElapsed.ToString("F");
        }
        else
        {
            timeElapsed = timeElapsed;
        }
    }

    public void RestartTimer()
    {
        timeElapsed = 45;
        timerText.gameObject.SetActive(false);
    }
}
