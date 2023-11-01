using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    
    public GameObject pauseMenuCanvas;
    public GameObject pauseMenu;

    public GameObject[] allMenus;

    private void Update()
    {
        PauseInteraction();
    }

    public void PauseInteraction()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0.0f)
            {
                Time.timeScale = 0.0f;
                pauseMenuCanvas.SetActive(true);
                pauseMenu.SetActive(true);
            }
            else if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
                pauseMenuCanvas.SetActive(false);
                pauseMenu.SetActive(true);

                for(int i = 0; i < allMenus.Length; i++)
                {
                    allMenus[i].SetActive(false);
                }
            }
        }
    }
}
