using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangCollectMovement : MonoBehaviour
{
    [SerializeField] float height = 1.8f;
    [SerializeField] float newScale = 0.7f;
    [SerializeField] float speed = 30f;
    private Vector3 startPosition;
    private float idleVelocity = 2.0f;
    private float idleDistance = 0.2f;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        float movement = Mathf.Sin(Time.time * idleVelocity) * idleDistance;
        transform.position = startPosition + new Vector3(0, movement, 0);
    }
}
