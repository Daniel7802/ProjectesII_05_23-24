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
        FadeInOutManager.instance.Fadeout();
        Cursor.lockState = CursorLockMode.Confined;
        if(pg != null)
        pg = pauseManager.GetComponent<PauseGameController>();
    }

    public void StartGame(string a)
    {
       FadeInOutManager.instance.Fadein();
        StartCoroutine(StartGameCorrotine(a));
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

    private IEnumerator StartGameCorrotine(string a)
    {
        Debug.Log("Waiting for loadScene");
        yield return new WaitWhile(delegate { return FadeInOutManager.instance.fadeIn; });
        SceneManager.LoadScene(a);
    }
}
