using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField]
    PlayerController _playerState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingZone"))
        {
            Debug.Log("patata");
            _playerState.playerStates = PlayerController.PlayerStates.FALLING;
        }
    }
     
}
