using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject eKey;

    [SerializeField]
    GameObject shopCanvas;

    public bool isShoping = false;

    [SerializeField]
    GameObject player;

    private PlayerInventory pi;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pi = player.GetComponent<PlayerInventory>();
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isShoping)
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                pc.playerStates = PlayerController.PlayerStates.CINEMATIC;
                shopCanvas.SetActive(true);
            }
        }
        else
        {
            shopCanvas.SetActive(false);
        }

        if(shopCanvas.activeInHierarchy == false)
        {
            pc.playerStates = PlayerController.PlayerStates.NONE;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("colisiona");
        if(collision.gameObject.tag.Equals("Player"))
        {
            eKey.SetActive(true);
            isShoping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            eKey.SetActive(false);
            isShoping = false;
        }
    }

    public void PurchaseHeart()
    {
        pi.IncreaseMaxHearts();
    }
}
