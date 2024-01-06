using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private GameObject NPC;

    private Animator NPCanimator;

    // Start is called before the first frame update
    void Start()
    {
        NPCanimator = NPC.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCanimator.SetBool("Greetings", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCanimator.SetBool("Greetings", false);
    }
}
