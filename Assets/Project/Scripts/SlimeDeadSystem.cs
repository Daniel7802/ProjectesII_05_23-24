using System.Collections;
using UnityEngine;


public class SlimeDeadSystem : DeadSystem
{
    [SerializeField] GameObject miniSlime;
    [SerializeField] float miniSlimeSpawnDistance = 2f;

    private Transform player;
    private Vector2 vectorBetweenPlayer, vectorSlime1, vectorSlime2;

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
        vectorBetweenPlayer = player.transform.position - transform.position;

        vectorSlime1 = new Vector2(-vectorBetweenPlayer.y, vectorBetweenPlayer.x).normalized * miniSlimeSpawnDistance;
        vectorSlime2 = new Vector2(vectorBetweenPlayer.y, -vectorBetweenPlayer.x).normalized * miniSlimeSpawnDistance;

        Instantiate(miniSlime, new Vector2(transform.position.x + vectorSlime1.x, transform.position.y + vectorSlime1.y), Quaternion.identity);
        Instantiate(miniSlime, new Vector2(transform.position.x + vectorSlime2.x, transform.position.y + vectorSlime2.y), Quaternion.identity);

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

