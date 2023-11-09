using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winManagerWithKeys : KeyManagers
{
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
    }
}
