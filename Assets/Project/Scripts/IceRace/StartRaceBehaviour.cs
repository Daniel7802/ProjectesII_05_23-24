using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRaceBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject eButton;

    [SerializeField]
    private GameObject timerText;

    [SerializeField]
    private GameObject player;

    public bool racing = false;

    private bool canRace = false;

    [SerializeField] private List<GameObject> raceObjects = new List<GameObject>();

    private PlayerController playerController;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (canRace)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                racing = true;
                timerText.gameObject.SetActive(true);
                foreach(GameObject o in raceObjects)
                {
                    o.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player") && playerController.playerStates != PlayerController.PlayerStates.CINEMATIC)
        {
            canRace = true;
            eButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canRace = false;
        eButton.gameObject.SetActive(false);
    }

    public void RestartRace()
    {
        racing = false;
        timerText.gameObject.SetActive(false);
    }
}
