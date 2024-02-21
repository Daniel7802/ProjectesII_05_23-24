using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashyIA : Enemy
{
    
    void Start()
    {

    }


    void Update()
    {
        FlipX();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case CurrentState.ROAMING:
                if (distanceToPlayer < startChasingRange && RaycastPlayer())
                {
                    
                }
                else
                {
                    Roaming();
                }
                break;

            case CurrentState.CHASING:

                break;

        }
    }
}
