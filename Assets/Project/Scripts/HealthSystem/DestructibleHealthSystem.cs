using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleHealthSystem : HealthSystem
{  
    DestructionEvent _destructionEvent;
    
    public override void Awake()
    {
        base.Awake();
        _destructionEvent = GetComponent<DestructionEvent>();
       
    }    

    public override void DeadState()
    {
        if(_destructionEvent != null)
        {
            _destructionEvent.DestroyItem();
            
        }
        else
        { }
    }
 
}
