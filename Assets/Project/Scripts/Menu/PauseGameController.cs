using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameController : MonoBehaviour
{
    public Canvas pauseMenuCanvas;
    public GameObject pauseMenu;

    public GameObject[] allMenus;

    public bool isPaused;

    [SerializeField]
    private GameObject Player;

    private PlayerController pc;

    private void Start()
    {
        pc = Player.GetComponent<PlayerController>();
    }

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
                Time.timeScale = 0.0f;
                pauseMenuCanvas.gameObject.SetActive(true);
                isPaused = true;
                pc.playerStates = PlayerController.PlayerStates.CINEMATIC;
            }
            else 
            {
                Time.timeScale = 1.0f;
                pauseMenuCanvas.gameObject.SetActive(false);             
                for(int i = 0; i < allMenus.Length; i++)
                {
                    allMenus[i].gameObject.SetActive(false);
                }
                pauseMenu.SetActive(true);
                isPaused = false;
                pc.playerStates = PlayerController.PlayerStates.NONE;
            }
        }
    }

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        pauseMenuCanvas.gameObject.SetActive(false);       

        isPaused = false;
        pc.playerStates = PlayerController.PlayerStates.NONE;
    }
    public void ExitMenu()
    {
        Time.timeScale = 1.0f;
        pauseMenuCanvas.gameObject.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }

}
