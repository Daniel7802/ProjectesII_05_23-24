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
   public CinemachineTargetGroup m_TargetGroup;
    [SerializeField]
    GameObject _boomerang;
    [SerializeField]
  public  GameObject _shadowBoomerang;
    [SerializeField]
  public  GameObject _iceBoomerang;



    public bool shadowBoomerangCollected = true;
    public bool iceBoomerangCollected = false;

 
    //[SerializeField]
    //GameObject _iceBoomerang;
    //[SerializeField]
    //GameObject _rootBoomerang;

  public GameObject actualBoomerang;

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
        ChangeBoomerang();
    }
 
    void ChangeBoomerang()
    {
        if (!_boomerangThrow.isFlying)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (actualBoomerang == _boomerang && iceBoomerangCollected)
                {
                    m_TargetGroup.RemoveMember(actualBoomerang.transform);

                    actualBoomerang.SetActive(false);
                    actualBoomerang = _iceBoomerang;

                    actualBoomerang.SetActive(true);
                }
                else if (actualBoomerang == _shadowBoomerang)
                {
                    m_TargetGroup.RemoveMember(actualBoomerang.transform);

                    actualBoomerang.SetActive(false);
                    actualBoomerang = _boomerang;

                    actualBoomerang.SetActive(true);
                }
                else if (actualBoomerang == _iceBoomerang && shadowBoomerangCollected)
                {
                    m_TargetGroup.RemoveMember(actualBoomerang.transform);

                    actualBoomerang.SetActive(false);
                    actualBoomerang = _shadowBoomerang;

                    actualBoomerang.SetActive(true);
                }
                StartCoroutine(AddToTargetGroup(actualBoomerang));
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (actualBoomerang == _boomerang && shadowBoomerangCollected)
                {
                    m_TargetGroup.RemoveMember(actualBoomerang.transform);

                    actualBoomerang.SetActive(false);
                    actualBoomerang = _shadowBoomerang;

                    actualBoomerang.SetActive(true);
                }
                else if (actualBoomerang == _shadowBoomerang && iceBoomerangCollected)
                {
                    m_TargetGroup.RemoveMember(actualBoomerang.transform);

                    actualBoomerang.SetActive(false);
                    actualBoomerang = _iceBoomerang;

                    actualBoomerang.SetActive(true);
                }
                else if (actualBoomerang == _iceBoomerang)
                {
                    m_TargetGroup.RemoveMember(actualBoomerang.transform);

                    actualBoomerang.SetActive(false);
                    actualBoomerang = _boomerang;

                    actualBoomerang.SetActive(true);
                }
                StartCoroutine(AddToTargetGroup(actualBoomerang));
            }
        } 
    }

    public IEnumerator AddToTargetGroup(GameObject a)
    {
        _boomerangThrow = a.GetComponent<BoomerangThrow>();
        yield return new WaitForSeconds(1.0f);
        m_TargetGroup.AddMember(a.transform, 0.55f, 5);
    }
}
