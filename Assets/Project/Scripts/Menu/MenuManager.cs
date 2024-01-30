using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]

    FadeInOutManager _fadeInOut;
    public GameObject pauseManager;

    private PauseGameController pg;

    private void Start()
    {
        _fadeInOut = GetComponent<FadeInOutManager>();
        Cursor.lockState = CursorLockMode.Confined;
        if(pg != null)
        pg = pauseManager.GetComponent<PauseGameController>();
    }

    public void StartGame(string a)
    {
        _fadeInOut.Fadein();
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
        yield return new WaitWhile(delegate { return _fadeInOut.fadeIn; });
        SceneManager.LoadScene(a);
    }
}
