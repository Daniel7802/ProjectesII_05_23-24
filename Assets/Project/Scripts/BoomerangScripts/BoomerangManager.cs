using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangManager : MonoBehaviour
{
    PlayerMovement _playerMovement;

    [SerializeField]
    GameObject _boomerang;
    [SerializeField]
    GameObject _shadowBoomerang;
    //[SerializeField]
    //GameObject _iceBoomerang;
    //[SerializeField]
    //GameObject _rootBoomerang;

    GameObject actualBoomerang;

    BoomerangThrow _boomerangThrow;

    float prepareLaunchSpeed = 60.0f;


    private void Awake()
    {       
        _playerMovement = GetComponent<PlayerMovement>();   
        actualBoomerang = _boomerang;
        _boomerangThrow = actualBoomerang.GetComponent<BoomerangThrow>();
    }
    private void Update()
    {
        prepareLaunch();
        ChangeBoomerang();
    }

    void prepareLaunch()
    {
        if(_boomerangThrow.mouseHold)
        {
            _playerMovement.speed = prepareLaunchSpeed;
        }
        else
        {
            _playerMovement.speed = _playerMovement.maxSpeed;
        }
    }
    void ChangeBoomerang()
    {
        if (!_boomerangThrow.isFlying)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (actualBoomerang == _boomerang)
                {
                    actualBoomerang.SetActive(false);
                    actualBoomerang = _shadowBoomerang;

                    actualBoomerang.SetActive(true);
                }
                else if (actualBoomerang == _shadowBoomerang)
                {
                    actualBoomerang.SetActive(false);
                    actualBoomerang = _boomerang;

                    actualBoomerang.SetActive(true);
                }

                _boomerangThrow = actualBoomerang.GetComponent<BoomerangThrow>();
            }
        } 
    }
}
