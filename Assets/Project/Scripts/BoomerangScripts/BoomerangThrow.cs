using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrow : MonoBehaviour
{
    private TargetJoint2D _targetJoint;

    [SerializeField]
    GameObject source; //Player


    [SerializeField]
    float maxTimer = 3.0f;
    [SerializeField]
    float timer;

   
    private float currentLerpValue;

    [SerializeField]
    private float rotationSpeed,minDistance,maxDistance, distance, throwDuration;
    

    [SerializeField]
    private bool coming, wantsToThrow, isFlying, going, mouseHold, rightMouse;


    [SerializeField]
    Vector2 p0, p2, pAux, vectorDirection, vectorObjective;

    private void Start()
    {
        minDistance = 2.8f;
        distance = minDistance;
        maxDistance = 8f;
        going = false;
    }

    void Awake()
    {
        _targetJoint = GetComponent<TargetJoint2D>();
    }
    private void Update()
    {
        MouseManager();
        if(mouseHold && distance <= maxDistance)
        {
            distance += Time.deltaTime * 3;
        }
        if (Input.GetMouseButtonDown(1))
        {
            rightMouse = true;
        }
    }
    private void FixedUpdate()
    {
    /*    if(!isFlying)
        {
            p0 = source.transform.position;
        }
       
        if (wantsToThrow && !isFlying)
        {
            currentLerpValue = 0f;
            wantsToThrow = false;
            ThrowBoomerang();
        }
        if (isFlying )
        {
            transform.Rotate(0f, 0f, rotationSpeed, Space.Self);
            if(coming)
                p0 = source.transform.position;
            timer = 3.0f;
            currentLerpValue = Mathf.Min(currentLerpValue + Time.fixedDeltaTime, throwDuration);
           
            float factor = currentLerpValue / throwDuration;

            factor = -Mathf.Abs(-1 + factor * 2) + 1;

            if (factor >= 0.8f)
            {
                coming = true;
            }
            Vector3 finalPos = Vector3.Lerp(p0, p2, factor);

            _targetJoint.anchor = Vector3.zero;
            _targetJoint.target = finalPos;

            isFlying = currentLerpValue < throwDuration;
        }
        else
        {
            _targetJoint.target = (Vector3)p0;
        }*/
        if(!isFlying)
        {
            p0 = source.transform.position;
            _targetJoint.target = (Vector3)p0;
        }
        if(wantsToThrow && !going)
        {
           ThrowBoomerang();
            timer = maxTimer;
            wantsToThrow = false;
            going = true;
            isFlying = true;
        }

        if (isFlying)
        {
            transform.Rotate(0f, 0f, rotationSpeed, Space.Self);
            if (going)
            {
                Going();
                going = false;
            }

            else if(!going && !coming)
            {
                Staying();
            }

            else if(coming)
            {
                Coming();
                isFlying = false;
                coming = false;
                going = false;
                timer = maxTimer;
                distance = minDistance;
            }
        }
    }
    void ThrowBoomerang()
    {
        rightMouse = false;
        p0 = transform.position; // 5,5         
        pAux = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vectorDirection = (Vector2)pAux-p0;
        vectorDirection.Normalize();
       vectorObjective = (vectorDirection) * distance + (Vector2)transform.position;
        //if (Vector2.Distance(transform.position, vectorObjective) <= Vector2.Distance(transform.position, pAux))
            p2 = vectorObjective;
       // else
        //    p2 = pAux;
        Debug.Log(p0 + " " + pAux);
        isFlying = true;
        Debug.Log(p2);
    }
    private void MouseManager()
    {
        if (Input.GetMouseButtonDown(0) && !isFlying)
        {
            mouseHold = true;            
        }
        if (Input.GetMouseButtonUp(0) && !isFlying)
        {
            wantsToThrow = true;
            mouseHold = false;
        }
    }

    //void TimerBoomerang()
    //{
    //    if (staticPoint && timer >= 0.5f)
    //    {
    //        timer -= Time.deltaTime;

    //    }
    //    else
    //        staticPoint = false;    
    //}

    void Going()
    {       
        Vector3 finalPos = Vector3.Lerp(p0, p2, throwDuration);
        _targetJoint.anchor = Vector3.zero;
        _targetJoint.target = finalPos;
    }

    void Staying()
    {
        if (timer >= 0f && rightMouse == false)
        {
            timer -= Time.deltaTime;           
        }
        else
            coming = true;
    }
    void Coming ()
    {
        p0 = source.transform.position;
        Vector2 comingPosition = Vector2.Lerp(p2, p0, throwDuration);
        _targetJoint.anchor = Vector3.zero;
        _targetJoint.target = comingPosition;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p0, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(p2, 0.1f);
    }
}

