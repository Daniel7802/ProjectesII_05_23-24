using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyPlayerDetection : PlayerDetection
{
    [SerializeField] float startShootingDistance = 8f;

    public bool shoot = false;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (chasing && Vector2.Distance(transform.position, playerPos.transform.position) < startShootingDistance)
        {
            chasing = false;
            shoot = true;
        }
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, startShootingDistance);
    }
}
