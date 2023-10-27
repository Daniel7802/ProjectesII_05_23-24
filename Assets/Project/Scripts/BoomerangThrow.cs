using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrow : MonoBehaviour
{
    private Rigidbody2D _physics;
    private TargetJoint2D _targetJoint;

    [SerializeField]
    GameObject source;

    private bool wantsToThrow;

    [SerializeField]
    private float throwDuration;
    private float currentLerpValue;


    public bool IsFlying = false;

    Vector2 p0, p1, p2, p3, p4;

    // Start is called before the first frame update
    void Awake()
    {
        _physics = GetComponent<Rigidbody2D>();
        _targetJoint = GetComponent<TargetJoint2D>();
    }
    private void Update()
    {
        wantsToThrow |= Input.GetMouseButtonDown(0) && !IsFlying;
    }
    private void FixedUpdate()
    {
        if(wantsToThrow && !IsFlying)
        {
            currentLerpValue = 0f;
            wantsToThrow = false;
            ThrowBoomerang();
        }

        p0 = source.transform.position;

        if (IsFlying)
        {
            currentLerpValue = Mathf.Min(currentLerpValue + Time.fixedDeltaTime, throwDuration);

            float factor = currentLerpValue / throwDuration;

            factor = -Mathf.Abs(-1 + factor * 2) + 1;
            Vector3 sourceToTarget = p2 - p0;

            if (factor <= 0.5f)
                p1 = Vector3.Cross(sourceToTarget, Vector3.forward).normalized * 3f + source.transform.position + sourceToTarget * 0.75f;
            else
                p1 = Vector3.Cross(Vector3.forward, sourceToTarget).normalized * 3f + source.transform.position + sourceToTarget * 0.75f;



            p3 = Vector3.Lerp(p0, p1, factor);
            p4 = Vector3.Lerp(p1, p2, factor);

            Vector3 finalPos = Vector3.Lerp(p3, p4, factor);

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
        p2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        IsFlying = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p0, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(p1, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(p2, 0.1f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(p3, 0.1f);
        Gizmos.DrawSphere(p4, 0.1f);
    }
}

