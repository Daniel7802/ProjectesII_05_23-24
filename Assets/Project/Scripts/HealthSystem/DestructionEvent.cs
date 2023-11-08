using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject _dropObject;

    SpriteRenderer _renderer;
    Collider2D _collider2D;

    public GameObject player;

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
        
        GameObject heart = Instantiate(_dropObject,_position, Quaternion.identity);
        heart.GetComponent<CollectableSystem>().SetTargetPosition(player);

        _renderer.enabled = false;
        _collider2D.enabled = false;        
    }
}
