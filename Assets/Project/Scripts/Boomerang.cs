using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField]
   protected float damage;
    [SerializeField]
    protected float speed;
    public Rigidbody2D rigidbody;
    public Rigidbody2D rigidbodyPlayer;
    [SerializeField]
    private Vector2 vectorMove;
    private Vector2 vectorDirection;
    public Vector2 _aimPoint;
    [SerializeField]
    public Vector2 _startPoint;
    [SerializeField]
    private bool comeBack;
    protected virtual void ActionBoomerang() { }
   
    public Boomerang(float d, float s)
    {
        damage = d; speed = s; comeBack = false;

}

protected void Movement()
    {
        if (!comeBack)
        {
            vectorDirection = (_aimPoint - _startPoint).normalized;
            vectorMove += vectorDirection * speed * Time.deltaTime;
            rigidbody.MovePosition(_startPoint + vectorMove);
        }
        else
        {
            vectorDirection = (rigidbodyPlayer.transform.position - rigidbody.transform.position).normalized;
            vectorMove += vectorDirection * speed * Time.deltaTime;
            rigidbody.MovePosition(vectorMove);
        }
    }
    protected void detectComeback()
    {
        if (_aimPoint.x <= rigidbody.transform.position.x && vectorMove.x <= _aimPoint.x)
        {
            comeBack = true;
        }
        else if (_aimPoint.x >= rigidbody.transform.position.x && vectorMove.x >= _aimPoint.x)
            comeBack = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_aimPoint, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rigidbody.position, 1);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_startPoint, 1);
    }

}
