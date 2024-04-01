using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBridge : MonoBehaviour
{
    [SerializeField]
   GameObject _bridgeBreak;
    [SerializeField]
    activateLeaver leaver;
    SpriteRenderer _spriteRenderer;
    BoxCollider2D _boxCollider;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(leaver.activate)
        {
            _spriteRenderer.enabled = true;
            _boxCollider.enabled = false;
        }
    }
   
}
