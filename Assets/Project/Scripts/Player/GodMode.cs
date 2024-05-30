using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    [SerializeField] 
    GameObject player;

    [SerializeField]
    private GameObject playerHealthSystem;

    [SerializeField]
    GameObject godModeCanvas;

    [SerializeField]
    private TextMeshProUGUI godEnabledText;

    private PlayerHealthSystem phs;
    private BoomerangManager bm;

    [SerializeField]
    Transform[] locations;

    // Start is called before the first frame update
    void Start()
    {
        phs = playerHealthSystem.GetComponent<PlayerHealthSystem>();
        bm = player.GetComponent<BoomerangManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(phs.isGod)
        {
            godEnabledText.text = "ON";
        }
        else if (!phs.isGod)
        {
            godEnabledText.text = "OFF";
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if(godModeCanvas.activeInHierarchy)
            {
                godModeCanvas.SetActive(false);
            }
            else if(!godModeCanvas.activeInHierarchy)
            {
                godModeCanvas.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            phs.RespawnHeal();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            player.transform.position = locations[0].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            player.transform.position = locations[1].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            player.transform.position = locations[2].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            bm.shadowBoomerangCollected = true;
            bm.iceBoomerangCollected = true;
        }
    }
}
