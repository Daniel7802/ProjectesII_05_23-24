using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFont : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    private PlayerHealthSystem phs;

    // Start is called before the first frame update
    void Start()
    {
        phs = player.GetComponent<PlayerHealthSystem>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            phs.RespawnHeal();
        }
    }
}
