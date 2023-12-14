using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(scene);
    }
}
