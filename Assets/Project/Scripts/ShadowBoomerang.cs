using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBoomerang : BoomerangThrow
{
    // Start is called before the first frame update
    [SerializeField]
    private ParticleSystem _particleBlackHole;
    PlayerMovement _playerMovement;
    [SerializeField]
    bool makeEffect = true;
    public override void  Start()
    {
        base.Start();   
        _playerMovement = source.GetComponent<PlayerMovement>();
        _particleBlackHole = GetComponent<ParticleSystem>();
    }

    new private void Update()
    {
        base.Update();
        Teleport();
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
        if (makeEffect)
        {
            _particleBlackHole.Play();
            makeEffect = false;
        }

        base.Staying();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && coming)
            makeEffect = true;
        base.OnTriggerEnter2D(collision);
      
    }
}
