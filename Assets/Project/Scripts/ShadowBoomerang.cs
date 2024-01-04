using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBoomerang : BoomerangThrow
{
    // Start is called before the first frame update
    [SerializeField]
    private ParticleSystem _particleBlackHole;
    [SerializeField]
    CircleCollider2D _circleCollider;
    [SerializeField]
    private GameObject _particleBlackHoleGO;
    PlayerMovement _playerMovement;
    [SerializeField]
    bool makeEffect = true;
    bool canTp = true;

    [SerializeField]private float  blackHoleForce = 30;
    public override void  Start()
    {
       
        base.Start();   
        _playerMovement = source.GetComponent<PlayerMovement>();
        _particleBlackHole = _particleBlackHoleGO.GetComponent<ParticleSystem>();
        _circleCollider.enabled = false;
    }

    new private void Update()
    {
        base.Update();
        Teleport();
        if(!coming && isFlying) 
        {
            _particleBlackHoleGO.transform.position = transform.position;
        }        
    }

    void Teleport ()
    {
        if(isFlying && Input.GetMouseButtonDown(0) && canTp)
        {
            _playerMovement.playerRb.MovePosition(transform.position);
            coming = true;
        }
    }
    protected override void Staying()
    {
        

        if (timer >= 0f)
        {
            if (timer <= 1.80f)
            {
                knockback = false;
               
            }

            if (timer <= 1.5f)
            {
                if (makeEffect)
                {
                    _circleCollider.enabled = true;
                    _particleBlackHole.Play();
                    makeEffect = false;
                }
            }

            if (timer < maxTimer - 0.2f && rightMouse == true)
            {
                coming = true;
                _particleBlackHole.Stop();
            }

            timer -= Time.deltaTime;
        }
        else
        {
            _particleBlackHole.Stop();
            _audioSource.PlayOneShot(goingSound);
            coming = true;
        }
    }
    protected override void Coming()
    {
        base.Coming();
        _circleCollider.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.CompareTag("Player") && coming)
        {
            canTp = true;
            makeEffect = true;
            _particleBlackHole.Stop();
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            canTp = false;
        }
            base.OnTriggerEnter2D(collision);
      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Water"))
        {
            canTp = true;
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") )
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 blackHoleForceDirection = transform.position - collision.transform.position;
            Vector2 blackHoleForceVector = blackHoleForceDirection.normalized * blackHoleForce;         
            rb.AddForce(blackHoleForceVector);
        }
    }
}
