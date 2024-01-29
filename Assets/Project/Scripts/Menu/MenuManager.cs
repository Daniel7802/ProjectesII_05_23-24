using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    

    public GameObject pauseManager;

    private PauseGameController pg;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if(pg != null)
        pg = pauseManager.GetComponent<PauseGameController>();
    }

    public void StartGame(string a)
    {
        SceneManager.LoadScene(a);
    }

    public void GoToMainMenu(string a)
    {
        SceneManager.LoadScene(a);
        Time.timeScale = 1.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        if (pg != null)
            pg.isPaused = false;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
