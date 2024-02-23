using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // SCRIPT SOLO PARA CONTROLAR LOS ESTADOS DEL PLAYER
    Rigidbody2D _rb;

    public enum PlayerStates { NONE, CINEMATIC }
    public PlayerStates playerStates;

    // Variables de scripts del player
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IceGround")
        {
            _rb.drag = 5;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IceGround")
        {
            _rb.drag = 20;
        }

    }
    // Variables para poder acceder desde otros scripts
}
