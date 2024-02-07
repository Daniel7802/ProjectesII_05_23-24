using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    
    public Canvas pauseMenuCanvas;
    public GameObject pauseMenu;

    public GameObject[] allMenus;

    public bool isPaused;

    private void Update()
    {
        PauseInteraction();
    }

    public void PauseInteraction()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                //Time.timeScale = 0.0f;
                pauseMenuCanvas.gameObject.SetActive(true);
                isPaused = true;
            }
            else 
            {
                Time.timeScale = 1.0f;
                pauseMenuCanvas.gameObject.SetActive(false);

                for(int i = 0; i < allMenus.Length; i++)
                {
                    allMenus[i].SetActive(false);
                }

                isPaused = false;
            }
        }
    }
}
