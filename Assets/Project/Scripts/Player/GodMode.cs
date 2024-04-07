using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    [SerializeField] 
    GameObject player;

    private PlayerHealthSystem phs;
    private BoomerangManager bm;

    [SerializeField]
    Transform[] locations;

    // Start is called before the first frame update
    void Start()
    {
        phs = player.GetComponent<PlayerHealthSystem>();
        bm = player.GetComponent<BoomerangManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            phs.health = 100;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.transform.position = locations[0].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.transform.position = locations[1].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
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
