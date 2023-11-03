using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrow : MonoBehaviour
{

    private Rigidbody2D _physics;
    private TargetJoint2D _targetJoint;

    [SerializeField]
    GameObject source; //Player

    private bool wantsToThrow;

    [SerializeField]
    float timer = 3.0f;

    [SerializeField]
    private float throwDuration = 0.5f;
    private float currentLerpValue;

    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float distancia;

    [SerializeField]
    private bool coming = false;

    public bool IsFlying = false;

    [SerializeField]
    Vector2 p0, p2;

    // Start is called before the first frame update
    void Awake()
    {
        _physics = GetComponent<Rigidbody2D>();
        _targetJoint = GetComponent<TargetJoint2D>();
    }
    private void Update()
    {
        wantsToThrow |= Input.GetMouseButtonDown(0) && !IsFlying;
        //TimerBoomerang();
    }
    private void FixedUpdate()
    {
       
        if (wantsToThrow && !IsFlying)
        {
            currentLerpValue = 0f;
            wantsToThrow = false;
            ThrowBoomerang();
        }
        if (IsFlying )
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

            IsFlying = currentLerpValue < throwDuration;
        }
        else
        {
            _targetJoint.target = (Vector3)p0;
        }
    }
    void ThrowBoomerang()
    {     

        p0 = transform.position; // 5,5     
        var auxPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var auxVector = (Vector2)auxPoint-p0;
        auxVector.Normalize();
        var auxVector2 = (auxVector) * distancia + (Vector2)transform.position;
        if (Vector2.Distance(transform.position, auxVector2) <= Vector2.Distance(transform.position, auxPoint))
            p2 = auxVector2;
        else
            p2 = auxPoint;
        Debug.Log(p0 + " " + auxPoint);
        IsFlying = true;
        Debug.Log(p2);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p0, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(p2, 0.1f);
    }
}

