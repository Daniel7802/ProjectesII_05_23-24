using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestructionObjectDeadSystem : DeadSystem
{
    [SerializeField]
    private GameObject _dropObject;
    [SerializeField]
    private GameObject player;
    private Vector3 _position;

    public override void Dead()
    {
        GameObject heart = Instantiate(_dropObject, transform.position, Quaternion.identity);
        heart.GetComponent<CollectableSystem>().SetTargetPosition(player);
        base.Dead();
       

    }
}
