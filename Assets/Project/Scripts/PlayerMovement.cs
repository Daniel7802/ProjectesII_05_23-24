using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

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

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Inputs
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            isRolling = true;
            startTimer = true;
        }

        movementVector = new Vector2(moveX, moveY).normalized;

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", movementVector.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Fisicas
        if(!isRolling)
        {
            playerRb.AddForce(Vector2.right.normalized * moveX * speed, ForceMode2D.Force);
            playerRb.AddForce(Vector2.up.normalized * moveY * speed, ForceMode2D.Force);
        }

        // Roll
        if (isRolling)
        {
            Vector2 rollVector = new Vector2(playerRb.velocity.x, playerRb.velocity.y);
            playerRb.AddForce(rollVector.normalized * ImpulseForce, ForceMode2D.Impulse);

            if (startTimer)
            {
                timer--;

                if(timer < 0)
                {
                    startTimer = false;
                    isRolling = false;
                    timer = 10;
                }
            }
        }
    }
}
