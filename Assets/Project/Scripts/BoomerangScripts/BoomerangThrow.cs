using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangThrow : MonoBehaviour
{    
    private TargetJoint2D _targetJoint;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    public CircleCollider2D _principalCircleCollider;

    private TrailRenderer _trailRenderer;
    private LineRenderer _lineRenderer;
    protected AudioSource _audioSource;

    public AudioClip goingSound;

    [SerializeField]
    protected GameObject source; //Player

    private ParticleSystem _particleSystemFire;
    private ParticleSystem.EmissionModule _missionModuleFire;

    private float rotationSpeed, minDistance, maxDistance, distance, throwDuration;
    protected float maxTimer = 3.0f, maxTimerAttack = 0.01f;
    protected float timer, timerTrail, attackTimer;
    protected float maxTimerTrail = 0.1f;


    bool cancelled;
    protected bool wantsToThrow, rightMouse, canThrow;
    public bool going, coming, knockback;
    public bool isFlying, isFire, mouseHold;

    Vector2 p0, p2, pAux, vectorDirection, vectorObjective;  

    
    protected void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _targetJoint = GetComponent<TargetJoint2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _trailRenderer = GetComponent<TrailRenderer>();       
        _particleSystemFire = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>(); 
        _particleSystemFire = GetComponent<ParticleSystem>();
        _missionModuleFire = _particleSystemFire.emission;
    }
    public virtual void Start()
    {
        rotationSpeed = 25;
        maxTimer = 2;
        maxTimerAttack = 0.05f;
        distance = 20;
        throwDuration = 1;
        minDistance = 2.8f;
        maxDistance = 8f;
        timerTrail = maxTimerTrail;
        attackTimer = maxTimerAttack;
        distance = minDistance;

        cancelled = false;
        going = false;
        canThrow = true;
        isFire = false;
        _particleSystemFire.Play();
    }

    protected void Update()
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
            Vector2 vectorOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            vectorOffset.Normalize();
            transform.position = (vectorOffset) * 0.05f + (Vector2)transform.position;
            if (distance <= maxDistance)
            distance += Time.deltaTime * 6;

        }
        if (Input.GetMouseButtonDown(1))
        {
            rightMouse = true;
            if (isFlying && !going) { _audioSource.PlayOneShot(goingSound); }
        }
    }
    protected void FixedUpdate()
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
           _principalCircleCollider.enabled = true;
            _spriteRenderer.enabled = true;
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
                Coming();          
            }
        }
        else
        {
            p0 = source.transform.position;
            _targetJoint.target = (Vector3)p0;
            StayTrailRenderer();
        }
    }
    protected void CalculateThrow()
    {
        p0 = source.transform.position; // 5,5         
        pAux = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vectorDirection = (Vector2)pAux - p0;
        vectorDirection.Normalize();
        vectorObjective = (vectorDirection) * distance + (Vector2)transform.position;
        p2 = vectorObjective;
    }   

    protected void ThrowBoomerang()
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
    protected void MouseManager()
    {
        if (Input.GetMouseButtonDown(0) && !isFlying && canThrow)
        {
            mouseHold = true;
        }
        if (Input.GetMouseButtonUp(0) && !isFlying && canThrow && mouseHold)
        {
            _principalCircleCollider.gameObject.SetActive(true);
            _spriteRenderer.gameObject.SetActive(true);
            mouseHold = false;
            wantsToThrow = true;
        }
    }

    protected void ShowTrayectoryLine()
    {
        if (mouseHold)
        {

            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, source.transform.position);
            _lineRenderer.SetPosition(1, vectorObjective);
        }
        else
            _lineRenderer.enabled = false;

    }
    protected void Going()
    {
        Vector3 finalPos = Vector3.Lerp(p0, p2, throwDuration);
        _targetJoint.anchor = Vector3.zero;
        _targetJoint.target = finalPos;
    }

    protected virtual void Staying()
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
    protected void StayTrailRenderer()
    {
        if (timerTrail >= 0f)
        {
            timerTrail -= Time.deltaTime;
        }
        else
            _trailRenderer.enabled = false; 
    }

    protected virtual void Coming()
    {
        _principalCircleCollider.enabled = true;
        p0 = source.transform.position;
        Vector2 comingPosition = Vector2.Lerp(p2, p0, throwDuration);
        _targetJoint.anchor = Vector3.zero;
        _targetJoint.target = comingPosition;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Wall")  || collision.gameObject.tag.Equals("ShadowWall"))
        {
            cancelled = true;
            coming = true;
            going = false;
            Coming();
        }
        if (collision.gameObject.CompareTag("Player") && (coming ||cancelled) )
        {
            
            cancelled = false;
            isFlying = false;
            coming = false;
            going = false;
            timer = maxTimer;
            distance = minDistance;
            _principalCircleCollider.enabled = false;
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
        
    }

   
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p0, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(p2, 0.1f);
    }
}