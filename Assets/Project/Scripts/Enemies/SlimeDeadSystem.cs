using System.Collections;
using UnityEngine;


public class SlimeDeadSystem : DeadSystem
{
    [SerializeField] GameObject miniSlime;
    [SerializeField] float spawnForce = 2f;

    private Transform player;

    public override void Dead()
    {

        if (!isDead)
        {
            _spriteRenderer.enabled = false;
            _collider.enabled = false;
            Instantiate(blood, transform.position, Quaternion.identity);
            SpawnMiniSlimes();
            StartCoroutine(PlayDeathClipOn(1));
            isDead = true;
        }
    }
    void SpawnMiniSlimes()
    {

        player = GetComponentInChildren<PlayerDetection>().playerPos;
        Vector2 vectorBetweenPlayer = player.transform.position - transform.position;
        Vector2 perp = new Vector2(vectorBetweenPlayer.y, -vectorBetweenPlayer.x);

        GameObject m1 = Instantiate(miniSlime, transform.position, Quaternion.identity);
        GameObject m2 = Instantiate(miniSlime, transform.position, Quaternion.identity);

        HealthSystem hp1 = m1.GetComponent<HealthSystem>();
        HealthSystem hp2 = m2.GetComponent<HealthSystem>();

        hp1.TurnInvencible(true);
        hp2.TurnInvencible(true);

        Rigidbody2D rb1 = m1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = m2.GetComponent<Rigidbody2D>();

        rb1.AddForce(perp.normalized * spawnForce, ForceMode2D.Impulse);
        rb2.AddForce(-perp.normalized * spawnForce, ForceMode2D.Impulse);


    }
    IEnumerator PlayDeathClipOn(float delay)
    {
        _audioSource.pitch = deathClipPitch;
        _audioSource.PlayOneShot(deathSound);
        yield return new WaitForSecondsRealtime(delay);
        _audioSource.pitch = 1f;
        Destroy(gameObject);
    }


}

