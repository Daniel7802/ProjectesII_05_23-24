using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string scene;
    [SerializeField]
    private GameObject hud;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FadeInOutManager.instance.Fadein();
        hud.SetActive(false);   
        StartCoroutine(StartGameCorrotine(scene));
    }


    private IEnumerator StartGameCorrotine(string a)
    {
        Debug.Log("Waiting for loadScene");
        yield return new WaitWhile(delegate { return FadeInOutManager.instance.fadeIn; });
        SceneManager.LoadScene(a);
    }
}
