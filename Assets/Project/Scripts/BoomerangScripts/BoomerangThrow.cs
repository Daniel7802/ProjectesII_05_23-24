using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrow : MonoBehaviour
{
    private TargetJoint2D _targetJoint;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private TrailRenderer _trailRenderer;
    private LineRenderer _lineRenderer;
    private AudioSource _audioSource;

    [SerializeField]
    GameObject source; //Player

    [SerializeField]
    private ParticleSystem _particleSystem;
    private ParticleSystem.EmissionModule _missionModule;


    [SerializeField]
    float maxTimer = 3.0f;
    [SerializeField]
    float timer, timerTrail;
    float maxTimerTrail = 0.1f;


    [SerializeField]
    private float rotationSpeed, minDistance, maxDistance, distance, throwDuration;


    [SerializeField]
    private bool coming, wantsToThrow, going, mouseHold, rightMouse, canThrow;
    public bool isFlying, isFire;

    [SerializeField]
    Vector2 p0, p2, pAux, vectorDirection, vectorObjective;

    [SerializeField]
    AudioClip enemyHitSound;

    private void Start()
    {
        minDistance = 2.8f;
        distance = minDistance;
        maxDistance = 8f;
        going = false;
        canThrow = true;
        timerTrail = maxTimerTrail;
        isFire = false;
    }

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _targetJoint = GetComponent<TargetJoint2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _missionModule = _particleSystem.emission;
        _trailRenderer = GetComponent<TrailRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        CalculateThrow();
        ShowTrayectoryLine();
        MouseManager();
        if (mouseHold)
        {
            transform.position = source.transform.position;
            if (distance <= maxDistance)
                distance += Time.deltaTime * 4;
        }
        if (Input.GetMouseButtonDown(1))
        {
            rightMouse = true;
        }
    }
    private void FixedUpdate()
    {
        if (wantsToThrow && !isFlying && canThrow)
        {
            ThrowBoomerang();
            timerTrail = maxTimerTrail;
            timer = maxTimer;
            wantsToThrow = false;
            going = true;
            isFlying = true;
        }

        if (isFlying)
        {
            _boxCollider.enabled = true;
            _spriteRenderer.enabled = true;
            _trailRenderer.startWidth = 0.37f;

            _trailRenderer.enabled = true;
            transform.Rotate(0f, 0f, rotationSpeed, Space.Self);
            if (going)
            {
                _missionModule.rateOverTime = 200;
                Going();
                going = false;
            }

            else if (!going && !coming)
            {
                _missionModule.rateOverTime = 20;
                Staying();
            }

            else if (coming)
            {
                _missionModule.rateOverTime = 200;
                Coming();
            }
        }
        else
        {
            _missionModule.rateOverTime = 0;
            p0 = source.transform.position;
            _targetJoint.target = (Vector3)p0;
            StayTrailRenderer();

        }
    }
    void CalculateThrow()
    {
        p0 = transform.position; // 5,5         
        pAux = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vectorDirection = (Vector2)pAux - p0;
        vectorDirection.Normalize();
        vectorObjective = (vectorDirection) * distance + (Vector2)transform.position;
        p2 = vectorObjective;
    }
    void ThrowBoomerang()
    {
        canThrow = false;
        rightMouse = false;

        Debug.Log(p0 + " " + pAux);
        isFlying = true;
        Debug.Log(p2);
    }
    private void MouseManager()
    {
        if (Input.GetMouseButtonDown(0) && !isFlying && canThrow)
        {
            mouseHold = true;
        }
        if (Input.GetMouseButtonUp(0) && !isFlying && canThrow && mouseHold)
        {
            _boxCollider.gameObject.SetActive(true);
            _spriteRenderer.gameObject.SetActive(true);
            mouseHold = false;
            wantsToThrow = true;
        }
    }

    void ShowTrayectoryLine()
    {
        if (mouseHold)
        {

            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, vectorObjective);
        }
        else
            _lineRenderer.enabled = false;

    }
    void Going()
    {
        Vector3 finalPos = Vector3.Lerp(p0, p2, throwDuration);
        _targetJoint.anchor = Vector3.zero;
        _targetJoint.target = finalPos;
    }

    void Staying()
    {
        going = false;
        if (timer >= 0f)
        {
            if (timer < maxTimer - 0.2f && rightMouse == true)
                coming = true;

            timer -= Time.deltaTime;
        }
        else
            coming = true;
    }
    void StayTrailRenderer()
    {
        if (timerTrail >= 0f)
        {
            timerTrail -= Time.deltaTime;
        }
        else
            _trailRenderer.startWidth = 0;
    }

    void Coming()
    {
        p0 = source.transform.position;
        Vector2 comingPosition = Vector2.Lerp(p2, p0, throwDuration);
        _targetJoint.anchor = Vector3.zero;
        _targetJoint.target = comingPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && coming)
        {
            isFlying = false;
            coming = false;
            going = false;
            timer = maxTimer;
            distance = minDistance;
            _boxCollider.enabled = false;
            _spriteRenderer.enabled = false;
            canThrow = true;
            isFire = false;
        }

        if (collision.gameObject.TryGetComponent<Torch>(out Torch torch))
        {
            if (torch.torchActive)
                isFire = true;
            else if (!torch.torchActive && isFire)
                torch.torchActive = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _audioSource.PlayOneShot(enemyHitSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            coming = true;
            going = false;
        }
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p0, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(p2, 0.1f);
    }
}

