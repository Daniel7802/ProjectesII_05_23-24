using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject _dropObject;

    SpriteRenderer _renderer;
    Collider2D _collider2D;

    Vector3 _position;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }
    private void Update()
    {
        _position = transform.position;   
    }
    public void DestroyItem()
    {
        Instantiate(_dropObject,_position, Quaternion.identity);
        _renderer.enabled = false;
        _collider2D.enabled = false;        
    }
}
