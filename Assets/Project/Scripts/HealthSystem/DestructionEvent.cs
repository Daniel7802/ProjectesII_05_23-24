using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject _dropObject;

    Renderer _renderer;
    Collider2D _collider2D;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider2D = GetComponent<Collider2D>();
    }
    public void Destroy()
    {
        _renderer.enabled = false;
        _collider2D.enabled = false;        
        Instantiate(_dropObject);
    }
}
