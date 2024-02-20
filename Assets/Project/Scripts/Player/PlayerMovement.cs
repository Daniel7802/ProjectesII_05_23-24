using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _walkParticles;
    private ParticleSystem.EmissionModule _missionModuleWalk;


    [SerializeField]
    public float speed = 10f;
    public float maxSpeed = 90f;

    public Rigidbody2D playerRb;
    private Animator playerAnimator;

    private Vector2 movementVector;
    private Vector2 movementVectorNormalized;

    //dash
    [SerializeField] private float _dashingVelocity = 50f;
    [SerializeField] private float _dashingTime = 0.02f;
    [SerializeField] private float _dashingCoolDownTime = 1f;
    private Vector2 _dashingDir;
    private bool _isDashing;
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

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

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

            if (playerRb.velocity.x != 0 || playerRb.velocity.y != 0)
            {
                _missionModuleWalk.enabled = true;
            }
            else
                _missionModuleWalk.enabled = false;
        }
        else
        {
            playerAnimator.SetFloat("Speed", 0);
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
        }
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
}
