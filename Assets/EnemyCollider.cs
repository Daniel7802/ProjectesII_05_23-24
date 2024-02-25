using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    GameObject parent;
    Rigidbody2D rb2D;
    public float knockbackForce;

    [SerializeField]
    GameObject hitParticles;

    private void Start()
    {
        parent = GetComponentInParent<GameObject>();
        rb2D = parent.GetComponent<Rigidbody2D>();
    }
    public void KnockBack(Vector2 dir)
    {
        Vector2 kbForce = dir.normalized * knockbackForce;
        rb2D.AddForce(kbForce, ForceMode2D.Impulse);
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boomerang"))
        {
            Vector2 dir = transform.position - collision.transform.position;
            KnockBack(dir);

            float angleRadians = Mathf.Atan2(dir.y, dir.x);

            // Convierte el ángulo a grados.
            float angleDegrees = angleRadians * Mathf.Rad2Deg;
            GameObject particles = Instantiate(hitParticles);
            particles.transform.SetParent(transform, true);
            if (GetComponent<LineRenderer>())
                particles.transform.localScale *= 2;

            particles.transform.position = transform.position;
            particles.transform.rotation = Quaternion.Euler(-angleDegrees, 90, -90);

            if (collision.GetComponentInParent<IceBoomerang>())
            {
                if (parent.GetComponent<Enemy>().canFreeze)
                    StartCoroutine(parent.GetComponent<Enemy>().Ice());
            }

        }

    }
}
