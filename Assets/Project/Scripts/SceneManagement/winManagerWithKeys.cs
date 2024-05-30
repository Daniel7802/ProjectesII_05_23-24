using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class winManagerWithKeys : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject[] keysOnHud;

    [SerializeField]
    Button finishGameButton;

    private PlayerInventory pI;

    private void Start()
    {
        pI = player.GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if(pI.CheckKeys() == 2)
        {
            keysOnHud[0].SetActive(true);
        }
        else if(pI.CheckKeys() >= 3)
        {
            keysOnHud[1].SetActive(true);
            finishGameButton.interactable = true;
        }
    }
}
