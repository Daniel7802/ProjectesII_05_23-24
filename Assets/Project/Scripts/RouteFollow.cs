using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector2 objectToMovePosition;

    private float speedModifier;

    private bool coroutineAllowed;
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
               
    }

    // Update is called once per frame
    void Update()
    {
        if(coroutineAllowed) 
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;

        while(tParam <1)
        {
            tParam += Time.deltaTime * speedModifier;

            transform.position = Mathf.Pow(1 - tParam, 2) * p0 + 2 * (1 - tParam) * tParam * p1 + Mathf.Pow(tParam, 2) * p2;

            //transform.position = objectToMovePosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        //if(routeToGo>routes.Length-1)
        //{
        //    routeToGo = 0;
        //}
        coroutineAllowed = true;
    }
}
