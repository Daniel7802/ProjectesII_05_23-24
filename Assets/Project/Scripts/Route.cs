using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0;t<=1; t+=0.05f)
        {
            gizmosPosition = Mathf.Pow(1-t,2) * controlPoints[0].position + 2*(1-t) * t * controlPoints[1].position+Mathf.Pow(t,2)*controlPoints[2].position;
            //Mathf.Pow(1 - tParam, 2) * p0 + 2 * (1 - tParam) * tParam * p1 + Mathf.Pow(tParam, 2) * p2;
            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(controlPoints[1].position.x, controlPoints[1].position.y), new Vector2(controlPoints[2].position.x, controlPoints[2].position.y));
    }

    
}
