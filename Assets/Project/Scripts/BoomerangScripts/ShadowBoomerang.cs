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
    [SerializeField]
    int waterOverlapped = 0;
    bool canTp { get { return waterOverlapped > 0; } }

    [SerializeField]private float  blackHoleForce = 30;
    public override void  Start()
    {
       
        base.Start();   
        _playerMovement = source.GetComponent<PlayerMovement>();
        _particleBlackHole = _particleBlackHoleGO.GetComponent<ParticleSystem>();
        _circleCollider.enabled = false;
        type = boomerangType.SHADOW;
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
        if(isFlying && Input.GetMouseButtonDown(1) && !canTp)
        {
            coming = true;
            _playerMovement.Teleport(transform.position);
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
                    canTouchWall = false;

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
            //_audioSource.PlayOneShot(goingSound);
            coming = true;
        }
    }
    protected override void ThrowBoomerang()
    {
        base.ThrowBoomerang();
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
            makeEffect = true;
            _particleBlackHole.Stop();
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            waterOverlapped++;
        }
            base.OnTriggerEnter2D(collision);
      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Water"))
        {
            waterOverlapped--;
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
