using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestructionObjectDeadSystem : DeadSystem
{
    [SerializeField]
    private GameObject _dropObject;    

    public override void Dead()
    {
        GameObject heart = Instantiate(_dropObject, transform.position, Quaternion.identity);
        base.Dead();  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
           if(collision.GetComponent<PlayerMovement>()._isDashing)
            Dead();
        }
    }
}
