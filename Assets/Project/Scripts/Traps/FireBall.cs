using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Bullet
{
    public GameObject fireballParticles;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
        {
            Instantiate(fireballParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.TryGetComponent<Torch>(out Torch torch) )
        {            
            torch.isActive = true;
            Instantiate(fireballParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    
}
