using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreachWave : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float maxRadius = 30f;

    private Rigidbody2D rb2D;

    private void FixedUpdate()
    {
        Vector3 a = new Vector3(1.0f, 1.0f, 0);
        if (transform.localScale.x < maxRadius)
        {
            transform.localScale += new Vector3(speed,speed,0);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
