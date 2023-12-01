using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBoomerang : BoomerangThrow
{
    // Start is called before the first frame update
    [SerializeField]
    private ParticleSystem _particleBlackHole;

    [SerializeField]
    private GameObject _particleBlackHoleGO;
    PlayerMovement _playerMovement;
    [SerializeField]
    bool makeEffect = true;
    public override void  Start()
    {
        base.Start();   
        _playerMovement = source.GetComponent<PlayerMovement>();
        _particleBlackHole = _particleBlackHoleGO.GetComponent<ParticleSystem>();
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
        if(isFlying && Input.GetMouseButtonDown(0))
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
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && !coming && !going)
        {
            Vector2 blackHoleForce = transform.position - collision.transform.position;
            blackHoleForce.Normalize();
            collision.attachedRigidbody.AddForce(blackHoleForce);
        }
        if (collision.gameObject.CompareTag("Player") && coming)
        {
            makeEffect = true;
            _particleBlackHole.Stop();
        }
            base.OnTriggerEnter2D(collision);
      
    }
}
