using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _walkParticles;
    private ParticleSystem.EmissionModule _missionModuleWalk;

    bool dontWalked = true;

    public float speed = 100f;
    public float maxSpeed = 100f;
    public float iceSpeedRecover = 100.0f;

    [SerializeField]
    AudioSource _walkSound;

    public Rigidbody2D playerRb;
    private Animator playerAnimator;
    private SpriteRenderer sr;

    private Vector2 movementVector;
    private Vector2 movementVectorNormalized;

    //dash
    private float _dashingVelocity = 35f;
    private float _dashingTime = 0.02f;
    private float _dashingCoolDownTime = 0.5f;
    private Vector2 _dashingDir;
    public bool _isDashing;
    private bool _canDash = true;
    private TrailRenderer _dashTrailRenderer;

    [SerializeField]
    private bool isRolling;

    [SerializeField]
    private float ImpulseForce;

    private PauseGameController pg;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip rollSound;

    // ESTADOS DEL PLAYER
    private PlayerController _playerController;

    private Vector2 teleportTarget;
    private bool wantsToTeleport;

    public enum lastDirection { LEFT, RIGHT, UP, DOWN, NONE };
    public lastDirection LastDirection;

    [SerializeField]
    private Sprite[] directionSprites;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        _audioSource = GetComponent<AudioSource>();
        _missionModuleWalk = _walkParticles.emission;
        _dashTrailRenderer = GetComponent<TrailRenderer>();

        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerController.playerStates != PlayerController.PlayerStates.CINEMATIC)
        {
            // Inputs
            movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Dash
            if (Input.GetKeyDown(KeyCode.Space) && movementVector != Vector2.zero && _canDash)
            {
                _audioSource.PlayOneShot(rollSound);

                _isDashing = true;
                _canDash = false;
                _dashTrailRenderer.enabled = true;

                _dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                StartCoroutine(StopDashing());
                StartCoroutine(DashCoolDown());
            }
            movementVectorNormalized = movementVector.normalized;

            playerAnimator.SetFloat("Horizontal", movementVectorNormalized.x);
            playerAnimator.SetFloat("Vertical", movementVectorNormalized.y);
            playerAnimator.SetFloat("Speed", movementVectorNormalized.sqrMagnitude);

            if (playerRb.velocity.x >= 0.1 || playerRb.velocity.y >= 0.1 || playerRb.velocity.x <= -0.1 || playerRb.velocity.y <= -0.1)
            {
                _missionModuleWalk.enabled = true;
                if(dontWalked)
                {
                    _walkSound.Play();
                    dontWalked = false;
                }
            }
            else
            {
                _walkSound.Stop();
                _missionModuleWalk.enabled = false;
                dontWalked = true;
            }
        }
        else
        {
            playerAnimator.SetFloat("Speed", 0);
        }

        if(speed < 0)
        {
            CheckLastDirection();
        }
    }

    private void FixedUpdate()
    {
        if (_playerController.playerStates != PlayerController.PlayerStates.CINEMATIC)
        {
            // Fisicas
            if (!_isDashing)
            {
                playerRb.AddForce(movementVectorNormalized * speed, ForceMode2D.Force);
            }

            // Roll
            if (_isDashing)
            {
                playerRb.velocity = _dashingDir.normalized * _dashingVelocity;

                return;
            }

            if (wantsToTeleport)
            {
                wantsToTeleport = false;
                transform.position = teleportTarget;
            }
        }
    }

    public void Teleport(Vector2 target)
    {
        wantsToTeleport = true;
        teleportTarget = target;
    }

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;
        _dashTrailRenderer.enabled = false;
    }
    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(_dashingCoolDownTime);
        _canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("IceGround"))
        {
            playerRb.drag = 2;
            speed = 20f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IceGround"))
        {
            playerRb.drag = 20;
            speed = iceSpeedRecover;
        }
    }

    private void CheckLastDirection()
    {
        if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            LastDirection = lastDirection.LEFT;
            SetAllFalse();
            playerAnimator.SetBool("Left", true);
        }
        else if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            LastDirection = lastDirection.RIGHT;
            SetAllFalse();
            playerAnimator.SetBool("Right", true);
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            LastDirection = lastDirection.UP;
            SetAllFalse();
            playerAnimator.SetBool("Up", true);
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            LastDirection = lastDirection.DOWN;
            SetAllFalse();
            playerAnimator.SetBool("Down", true);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            // DOWN LEFT
            SetAllFalse();
            playerAnimator.SetBool("DownLeft", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            // UP LEFT
            SetAllFalse();
            playerAnimator.SetBool("UpLeft", true);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            // UP RIGHT
            SetAllFalse();
            playerAnimator.SetBool("UpRight", true);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            // DOWN RIGHT
            SetAllFalse();
            playerAnimator.SetBool("DownRight", true);
        }

        if (speed < 0.001)
        {
            if(LastDirection == lastDirection.LEFT)
            {
                sr.sprite = directionSprites[0];
            }
            else if (LastDirection == lastDirection.RIGHT)
            {
                sr.sprite = directionSprites[1];
            }
            else if (LastDirection == lastDirection.UP)
            {
                sr.sprite = directionSprites[2];
            }
            else if (LastDirection == lastDirection.DOWN)
            {
                sr.sprite = directionSprites[3];
            }
        }
    }

    private void SetAllFalse()
    {
        playerAnimator.SetBool("DownRight", false);
        playerAnimator.SetBool("UpRight", false);
        playerAnimator.SetBool("UpLeft", false);
        playerAnimator.SetBool("DownLeft", false);
        playerAnimator.SetBool("Down", false);
        playerAnimator.SetBool("Up", false);
        playerAnimator.SetBool("Right", false);
        playerAnimator.SetBool("Left", false);
    }
}
