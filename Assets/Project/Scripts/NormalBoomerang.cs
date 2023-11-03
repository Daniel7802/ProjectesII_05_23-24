using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalBoomerang : Boomerang
{               
    private void Start()
    {
        ThrowBoomerang();
    }
    private void FixedUpdate()
    {
        BoomerangMovement();
    }
}
