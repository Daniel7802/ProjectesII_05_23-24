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
            actualBoomerang.GetComponent<Boomerang>().source = this.gameObject;
            actualBoomerang.GetComponent<Boomerang>().pAim = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            _canThrow = false;
        }                
    }

   
}
