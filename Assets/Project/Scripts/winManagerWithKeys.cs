using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winManagerWithKeys : KeyManagers
{
    [SerializeField]
    GameObject[] keysHUD;

    private KeyManagers km;

    private void Start()
    {
        km = GetComponent<KeyManagers>();
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

        for(int i = 0; i < km.counter; i++)
        {
            keysHUD[i].SetActive(true);
        }
    }
}
