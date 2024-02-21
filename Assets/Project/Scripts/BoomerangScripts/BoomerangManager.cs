using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangManager : MonoBehaviour
{
    PlayerMovement _playerMovement;
    [SerializeField]
    CinemachineTargetGroup m_TargetGroup;
    [SerializeField]
    GameObject _boomerang;
    [SerializeField]
    GameObject _shadowBoomerang;

    public bool shadowBoomerangCollected = false;

 
    //[SerializeField]
    //GameObject _iceBoomerang;
    //[SerializeField]
    //GameObject _rootBoomerang;

   GameObject actualBoomerang;

    public BoomerangThrow _boomerangThrow;

    float prepareLaunchSpeed = 60.0f;


    private void Awake()
    {
       
        _playerMovement = GetComponent<PlayerMovement>();   
        actualBoomerang = _boomerang;
        _boomerangThrow = actualBoomerang.GetComponent<BoomerangThrow>();
       
    }

    private void Start()
    {
        if(m_TargetGroup != null)
            m_TargetGroup.AddMember(actualBoomerang.transform, 0.55f, 5);
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
            if (Input.GetKeyDown(KeyCode.Q) && shadowBoomerangCollected)
            {
                m_TargetGroup.RemoveMember(actualBoomerang.transform);
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
                StartCoroutine(AddToTargetGroup(actualBoomerang));
            }
        } 
    }

    private IEnumerator AddToTargetGroup(GameObject a)
    {
        _boomerangThrow = a.GetComponent<BoomerangThrow>();
        yield return new WaitForSeconds(0.2f);
        m_TargetGroup.AddMember(a.transform, 0.55f, 5);
    }
}
