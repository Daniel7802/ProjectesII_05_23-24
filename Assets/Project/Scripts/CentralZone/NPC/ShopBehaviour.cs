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

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject[] extraHearts;

    private int extraheartsCounter = 0;

    private PlayerHealthSystem phs;

    public TextMeshProUGUI currentRootsText;

    public int currentRoots = 30;

    public bool isShoping = false;

    // Start is called before the first frame update
    void Start()
    {
        currentRootsText.text = currentRoots.ToString();
        phs = Player.transform.GetComponent<PlayerHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isShoping)
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                shopCanvas.SetActive(true);
            }
        }
        else
        {
            shopCanvas.SetActive(false);
        }

        Debug.Log(currentRoots);
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
        if(currentRoots >= 5)
        {
            currentRoots -= 5;
            currentRootsText.text = currentRoots.ToString();

            phs.MaxHealth++;
            extraHearts[extraheartsCounter].SetActive(true);
            extraheartsCounter++;
            phs.health = phs.MaxHealth;
            phs.RespawnHeal();
        }
    }
}
