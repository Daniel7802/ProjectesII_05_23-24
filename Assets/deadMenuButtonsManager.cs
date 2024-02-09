using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deadMenuButtonsManager : MonoBehaviour
{
    [SerializeField]
    Canvas deadMenuCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        FadeInOutManager.instance.Fadeout();
        deadMenuCanvas.gameObject.SetActive(false);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
