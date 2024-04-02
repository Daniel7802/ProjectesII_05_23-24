using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winManagerWithKeys : KeyManagers
{
    [SerializeField]
    GameObject[] keysHUD;

    [SerializeField]
    GameObject keyManager;

    public int counter = 0;

    private void Start()
    {
    }

    public override void Update()
    {
        for (int i = 0; i < key.Length; i++)
        {
            activeMechanisme = key[i].taken;
        }

        if (activeMechanisme)
        {
            SceneManager.LoadScene("MainMenu");
        }

        for(int i = 0; i < counter; i++)
        {
            keysHUD[i].SetActive(true);
        }
    }
}
