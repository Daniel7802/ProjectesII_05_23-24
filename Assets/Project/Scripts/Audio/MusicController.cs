using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private GameObject mapMusic;

    [SerializeField]
    private GameObject raceMusic;

    [SerializeField]
    private GameObject raceManager;

    private StartRaceBehaviour startRaceBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        startRaceBehaviour = raceManager.GetComponent<StartRaceBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startRaceBehaviour.racing)
        {
            mapMusic.gameObject.SetActive(false);
            raceMusic.gameObject.SetActive(true);
        }
        else
        {
            mapMusic.gameObject.SetActive(true);
            raceMusic.gameObject.SetActive(false);
        }
    }
}
