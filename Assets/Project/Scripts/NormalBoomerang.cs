using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalBoomerang : Boomerang
{
    public NormalBoomerang(float d, float s) : base(d, s) { }      
        
    protected override void ActionBoomerang()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
    }
    private void FixedUpdate()
    {
        Movement();
        //detectComeback();
    }
}
