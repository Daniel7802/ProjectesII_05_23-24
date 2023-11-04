using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private TargetJoint2D _targetJoint;

    [SerializeField]
    public GameObject source; //Player

    [SerializeField]
    protected float timer, throwDuration, distance, rotationSpeed;
    float currentLerpValue;

    [SerializeField]
    private bool coming, isFlying, wantsToThrow;

    [SerializeField]
    Vector2 p0, p2, pHit;
    public Vector2 pAim;

    [SerializeField]
    Vector2 vectorDirection;


    private void Awake()
    {
        _targetJoint = GetComponent<TargetJoint2D>();
    }

    protected virtual void Start()
    {
        wantsToThrow = true;    
        timer = 3.0f;
        coming = false;
        isFlying = false;  
        rotationSpeed = 30;
        currentLerpValue = 0f;
    }


    protected virtual void FixedUpdate()
    {
        wantsToThrow |= Input.GetMouseButtonDown(0) && !isFlying;
        if (wantsToThrow && !isFlying)
        {
            currentLerpValue = 0f;
            wantsToThrow = false;
            BoomerangMovement();

        }
    }
    public virtual void BoomerangMovement()
    {              
        if (isFlying)
        {
            transform.Rotate(0f, 0f, rotationSpeed, Space.Self);
            if (coming)
            { p0 = source.transform.position;}

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
        }
    }

    public virtual void ThrowBoomerang()
    {
        //PAim lo tiene que asignar el boomerangManager, a partir de ahi se calcula todo
        p0 = source.transform.position; // 5,5     
        vectorDirection = (Vector2)pAim - p0;
        vectorDirection.Normalize();
        pHit = (vectorDirection) * distance + (Vector2)transform.position;

        if (Vector2.Distance(transform.position, pHit) <= Vector2.Distance(transform.position, vectorDirection))
        { p2 = pHit; }
        else
        { p2 = vectorDirection; }

        Debug.Log(p0 + " " + vectorDirection);
        isFlying = true;
        Debug.Log(p2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pAim, 0.3f);
    }
}
