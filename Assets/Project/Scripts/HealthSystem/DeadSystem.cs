using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class DeadSystem : MonoBehaviour
{
    public GameObject blood;
    public AudioClip deathSound;
    [SerializeField]
    protected AudioSource _audioSource;
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider;

    [SerializeField]
    protected LineRenderer _lineRenderer;
    [SerializeField] protected float deathClipPitch = 1f;
    public bool isDead = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public virtual void Dead()
    {
        if (!isDead)
        {
            _spriteRenderer.enabled = false;
            _collider.enabled = false;

            if (_lineRenderer != null)
            {
                Destroy(_lineRenderer);
                //EditorApplication.isPaused = true;
            }
            //Enemy enemy = GetComponent<Enemy>();
            //if (enemy != null)
            //{
            //    enemy.foundTargetAlert.enabled = false;
            //    enemy.lostTargetAlert.enabled = false;

            //}
            Light2D light2D = GetComponentInChildren<Light2D>();

            if (light2D != null)
            {
                light2D.enabled = false;
            }


            StartCoroutine(PlayDeathClipOn(1));
            Instantiate(blood, transform.position, Quaternion.identity);
            isDead = true;
        }

    }

    IEnumerator PlayDeathClipOn(float delay)
    {
        _audioSource.pitch = deathClipPitch;
        _audioSource.PlayOneShot(deathSound);
        yield return new WaitForSecondsRealtime(delay);
        _audioSource.pitch = 1f;
        Destroy(gameObject);
    }

}
