using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangManager : MonoBehaviour
{
    [SerializeField]
    GameObject _boomerang;
    [SerializeField]
    BoomerangThrow _boomerangThrow;
        
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_boomerangThrow.isFlying)
        {
            _boomerang.SetActive(true);
        }
    }
}
