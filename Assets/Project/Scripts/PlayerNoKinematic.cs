using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoKinematic : MonoBehaviour
{
    private PlayerController pc;

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        pc = player.GetComponent<PlayerController>();
    }

    public void NoKinematic()
    {
        pc.playerStates = PlayerController.PlayerStates.NONE;
    }
}
