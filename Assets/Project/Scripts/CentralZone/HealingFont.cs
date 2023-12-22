using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFont : MonoBehaviour
{
    [SerializeField]
    GameObject[] hearts;

    [SerializeField]
    GameObject player;

    private PlayerHealthSystem phs;

    // Start is called before the first frame update
    void Start()
    {
        phs = player.GetComponent<PlayerHealthSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        phs.counter = 0;

        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(true);
        }
    }
}
