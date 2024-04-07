using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winManagerWithKeys : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject[] keysOnHud;

    [SerializeField]
    GameObject finishGameButton;

    private PlayerInventory pI;

    private void Start()
    {
        pI = player.GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if(pI.CheckKeys() == 1)
        {
            keysOnHud[0].SetActive(true);
        }
        else if(pI.CheckKeys() == 2)
        {
            keysOnHud[1].SetActive(true);
            finishGameButton.SetActive(true);
        }
    }
}
