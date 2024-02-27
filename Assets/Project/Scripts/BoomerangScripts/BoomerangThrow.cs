using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangThrow : MonoBehaviour
{
    protected TargetJoint2D _targetJoint;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    public CircleCollider2D _principalCircleCollider;
    protected bool canTouchWall = true;

    [SerializeField]
    private TrailRenderer _trailRenderer;
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    protected AudioSource _audioSource;

    [SerializeField]
    public AudioClip goingSound;

    [SerializeField]
    protected GameObject source; //Player

    [SerializeField]
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

   public enum boomerangType { NORMAL, SHADOW, ICE, RAIZ};


    public boomerangType type;
    [SerializeField]
    //GameObject shopManager;

    // private ShopBehaviour sb;

    // ESTADOS DEL PLAYER
    private PlayerController _playerController;

    protected void Awake()
    {
        _targetJoint = GetComponent<TargetJoint2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _missionModuleFire = _particleSystemFire.emission;

        //sb = shopManager.GetComponent<ShopBehaviour>();
        _playerController = source.GetComponent<PlayerController>();
    }
    public virtual void Start()
    {
        type = boomerangType.NORMAL;
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
        if (_playerController.playerStates == PlayerController.PlayerStates.CINEMATIC)
        {
            return;
        }

        if (isFire)
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
        if (Input.GetMouseButtonDown(0) && isFlying)
        {
            rightMouse = true;
            if (isFlying && !going) { 
                _audioSource.PlayOneShot(goingSound);
            }
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

    protected virtual void ThrowBoomerang()
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

        if (collision.gameObject.tag.Equals("Wall") && canTouchWall == true || collision.gameObject.tag.Equals("ShadowWall") && canTouchWall == true)
        {
            cancelled = true;
            coming = true;
            going = false;
            Coming();
        }
        if (collision.gameObject.CompareTag("Player") && (coming || cancelled))
        {
            canTouchWall = true;
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

        if (collision.gameObject.TryGetComponent<Torch>(out Torch torch) && isFlying && type == boomerangType.NORMAL)
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
        Gizmos.DrawSphere(p0, 0.5f);
        Gizmos.DrawSphere(pAux, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(p2, 0.1f);
    }
}