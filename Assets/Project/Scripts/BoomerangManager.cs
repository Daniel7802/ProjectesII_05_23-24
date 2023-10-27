using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangManager : MonoBehaviour
{
    [SerializeField]
    private bool _canThrow;

    public GameObject boomerang;

    [SerializeField]
    private Transform throwPos;


    void Start()
    {
        _canThrow = true;
    }

    void Update()
    {          
        if (Input.GetMouseButtonDown(0) && _canThrow)
        {
           GameObject actualBoomerang = Instantiate(boomerang, throwPos.position, Quaternion.identity);
            actualBoomerang.GetComponent<Boomerang>()._aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            actualBoomerang.GetComponent<Boomerang>().rigidbody.position = throwPos.position;
            actualBoomerang.GetComponent<Boomerang>()._startPoint = throwPos.position;
            _canThrow = false;
        }                
    }

   
}
