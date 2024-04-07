using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoTimerAnimation : MonoBehaviour
{
    private Vector3 startPosition;
    private float idleVelocity = 2.0f;
    private float idleDistance = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float movement = Mathf.Sin(Time.time * idleVelocity) * idleDistance;
        transform.position = startPosition + new Vector3(0, movement, 0);
    }
}
