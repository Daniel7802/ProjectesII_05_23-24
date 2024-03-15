using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRaceBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject eButton;

    [SerializeField]
    private GameObject timerText;

    public bool racing = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            eButton.gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                racing = true;
                timerText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        eButton.gameObject.SetActive(false);
    }

    public void RestartRace()
    {
        racing = false;
        timerText.gameObject.SetActive(false);
    }
}
