using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorMechanism : MonoBehaviour
{
    SpriteRenderer _renderer;
    BoxCollider2D _boxCollider;
    [SerializeField]
    private MecanismPressure[] mecanism;

    [SerializeField]
    private bool activeMecanisme;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>(); 
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        for (int i = 0; i < mecanism.Length; i++)
        {
            activeMecanisme = mecanism[i].activate;
        }

        if (activeMecanisme)
        {
            _renderer.enabled = false;
            _boxCollider.enabled = false;
        }
        else
        {
            _renderer.enabled = true;
            _boxCollider.enabled = true;
        }
    }
}
