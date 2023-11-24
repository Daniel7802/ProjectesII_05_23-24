using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangThrow : MonoBehaviour
{
    private TargetJoint2D _targetJoint;
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private BoxCollider2D _boxCollider;
    private CircleCollider2D _circleCollider;


    private TrailRenderer _trailRenderer;
    private LineRenderer _lineRenderer;
    private AudioSource _audioSource;

    public AudioClip goingSound;


    [SerializeField]
    GameObject source; //Player

    [SerializeField]
    private ParticleSystem _particleSystemFire;
    private ParticleSystem.EmissionModule _missionModuleFire;


    [SerializeField]
    float maxTimer = 3.0f, maxTimerAttack = 0.01f;
    [SerializeField]
    float timer, timerTrail, attackTimer;
    float maxTimerTrail = 0.1f;


    [SerializeField]
    private float rotationSpeed, minDistance, maxDistance, distance, throwDuration;


    [SerializeField]
    private bool wantsToThrow, rightMouse, canThrow;
    [SerializeField]
    public bool going, coming, knockback;
    public bool isFlying, isFire, mouseHold;

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
        _circleCollider.enabled = false;
        attackTimer = maxTimerAttack;
    }

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _targetJoint = GetComponent<TargetJoint2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _circleCollider = GetComponent<CircleCollider2D>(); 
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _trailRenderer = GetComponent<TrailRenderer>();

        _particleSystemFire = GetComponent<ParticleSystem>();

        _missionModuleFire = _particleSystemFire.emission;
      
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {

        if(isFire)
        {
            _missionModuleFire.enabled = true;
        }
        else
            _missionModuleFire.enabled = false;

    
        CalculateThrow();
        ShowTrayectoryLine();
        MouseManager();
        if (mouseHold)
        {
            transform.position = source.transform.position;
            if(distance <= maxDistance)
            distance += Time.deltaTime * 6;
        }
        if (Input.GetMouseButtonDown(1))
        {
            rightMouse = true;
            if (isFlying && !going) { _audioSource.PlayOneShot(goingSound); }
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
                Going();
                going = false;
            }

            else if (!going && !coming)
            {
                Staying();
            }

            else if (coming)
            {
                AttackArea();              
            }
        }
        else
        {
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

    void AttackArea()
    {
        coming = true;
        _boxCollider.enabled = false;
        _circleCollider.enabled = true;

        if (attackTimer >= 0.0f)
            attackTimer -= Time.deltaTime;
        else
        Coming();
    }

    void ThrowBoomerang()
    {
        knockback = true;
        going = true;
        _audioSource.PlayOneShot(goingSound);
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
        if (timer >= 0f)
        {
            if (timer <= 1.80f)
                knockback = false;
            if (timer < maxTimer - 0.2f && rightMouse == true)
                coming = true;

            timer -= Time.deltaTime;
        }
        else
        {
            _audioSource.PlayOneShot(goingSound);
            coming = true;
        }

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
        _circleCollider.enabled = false;
        _boxCollider.enabled = true;
        p0 = source.transform.position;
        Vector2 comingPosition = Vector2.Lerp(p2, p0, throwDuration);
        _targetJoint.anchor = Vector3.zero;
        _targetJoint.target = comingPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Wall"))
        {
            coming = true;
            going = false;
        }
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
            attackTimer = maxTimerAttack;
        }

        if (collision.gameObject.TryGetComponent<Torch>(out Torch torch) && isFlying)
        {
            if (torch.torchActive)
                isFire = true;
            else if (!torch.torchActive && isFire)
                torch.torchActive = true;
        }
        if (collision.gameObject.CompareTag("Enemy")&&_spriteRenderer.enabled)
        {
            _audioSource.PlayOneShot(enemyHitSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            _audioSource.PlayOneShot(goingSound);
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

