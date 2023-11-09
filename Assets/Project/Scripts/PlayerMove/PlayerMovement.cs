using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    BoomerangThrow _boomerangThrow;

    private Rigidbody2D playerRb;
    private Animator playerAnimator;

    private float moveX;
    private float moveY;

    private Vector2 movementVector;

    [SerializeField]
    private bool isRolling;

    [SerializeField]
    private float ImpulseForce;

    private float timer = 10;
    private bool startTimer = false;
    private bool rollCoolDown = false;

    private PauseGameController pg;
    [SerializeField]
    GameObject pauseManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        pg = pauseManager.GetComponent<PauseGameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pg.isPaused)
        {
            // Inputs
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");

            if (_boomerangThrow.mouseHold)
            {
                speed = 60f;
            }
            else
            {
                speed = 90f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isRolling && !rollCoolDown && !_boomerangThrow.mouseHold && (moveX != 0 || moveY != 0))
            {
                isRolling = true;
                startTimer = true;
            }

            movementVector = new Vector2(moveX, moveY).normalized;

            playerAnimator.SetFloat("Horizontal", moveX);
            playerAnimator.SetFloat("Vertical", moveY);
            playerAnimator.SetFloat("Speed", movementVector.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        if(!pg.isPaused)
        {
            // Fisicas
            if (!isRolling)
            {
                playerRb.AddForce(movementVector * speed, ForceMode2D.Force);
            }

            // Roll
            if (isRolling)
            {
                Vector2 rollVector = new Vector2(playerRb.velocity.x, playerRb.velocity.y);
                playerRb.AddForce(rollVector.normalized * ImpulseForce, ForceMode2D.Impulse);

                if (startTimer)
                {
                    timer--;

                    if (timer < 0)
                    {
                        startTimer = false;
                        isRolling = false;
                        rollCoolDown = true;
                    }
                }
            }

            if (rollCoolDown)
            {
                timer++;

                if (timer >= 30)
                {
                    isRolling = false;
                    rollCoolDown = false;
                    timer = 10;
                }
            }
        }
    }
}
