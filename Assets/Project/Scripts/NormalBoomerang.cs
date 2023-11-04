using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalBoomerang : Boomerang
{               
    protected override void Start()
    {
        base.Start();
        distance = 6;
        throwDuration = 1f;
        ThrowBoomerang();
    }

}
