using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRaceBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject raceNPC;

    [SerializeField]
    private GameObject eButton;

    public bool raceStarted = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            eButton.gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                raceStarted = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        eButton.gameObject.SetActive(false);
    }
}
