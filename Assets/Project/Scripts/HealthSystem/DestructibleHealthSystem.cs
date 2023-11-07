using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DestructibleHealthSystem : HealthSystem
{
    DestructionEvent _destructionEvent;
    public override void Awake()
    {
        base.Awake();
        _destructionEvent.GetComponent<DestructionEvent>();
    }

    public override void DeadState()
    {
        _destructionEvent.Destroy();
    }
   
}
