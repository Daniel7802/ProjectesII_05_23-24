using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBoomerang : BoomerangThrow
{
    [SerializeField]
    CircleCollider2D _circleCollider;
    [SerializeField]
    PlayerMovement _playerMovement;

    [SerializeField]
    GameObject _blackHole;
    ParticleSystem _supplySystem;

    bool makeEffect = true;

    int waterOverlapped = 0;

    private float blackHoleForce = 30;
    bool canTp { get { return waterOverlapped > 0; } }
    
    
    [SerializeField]
    private AudioClip tpSound;

    
    public override void Start()
    {

        base.Start();
        _playerMovement = source.GetComponent<PlayerMovement>();
        _circleCollider.enabled = false;
        type = BoomerangType.SHADOW;
    }

    new private void Update()
    {
        base.Update();
        Teleport();       
    }

    void Teleport()
    {
        if (isFlying && Input.GetMouseButtonDown(1) && !canTp)
        {
            _audioSource.PlayOneShot(tpSound);
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
                   GameObject a =  Instantiate(_blackHole, this.transform.position, Quaternion.identity);
                    _supplySystem = a.GetComponent<ParticleSystem>();
                    makeEffect = false;
                }
            }

            if (timer < maxTimer - 0.2f && rightMouse == true)
            {
                coming = true;
               _supplySystem.Stop();
            }

            timer -= Time.deltaTime;
        }
        else
        {
            //_particleBlackHole.Stop();
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
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            waterOverlapped++;
        }
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("Fire"))
        {
            coming = true;
        }


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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 blackHoleForceDirection = transform.position - collision.transform.position;
            Vector2 blackHoleForceVector = blackHoleForceDirection.normalized * blackHoleForce;
            rb.AddForce(blackHoleForceVector);
        }
    }
}
