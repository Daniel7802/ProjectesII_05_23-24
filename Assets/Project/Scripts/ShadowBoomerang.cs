using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBoomerang : BoomerangThrow
{
    // Start is called before the first frame update
    PlayerMovement _playerMovement;

    public override void  Start()
    {
        base.Start();   
        _playerMovement = source.GetComponent<PlayerMovement>();
    }

    new private void Update()
    {
        base.Update();
        Teleport();
    }
    
    void Teleport ()
    {
        if(isFlying && Input.GetMouseButtonDown(0))
        {
            _playerMovement.playerRb.MovePosition(transform.position);
            coming = true;
        }

    }
    
}
