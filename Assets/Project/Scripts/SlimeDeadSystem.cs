using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeDeadSystem : DeadSystem
{
    [SerializeField] GameObject miniSlime;
    bool miniSlimesSpawned = false;

    [SerializeField] float spawnForce = 10f;

    private GameObject player;
    private Vector2 vectorBetweenPlayer, vectorSlime1, vectorSlime2;

    public override void Dead()
    {
        base.Dead();
        SpawnMiniSlimes();


    }
    void SpawnMiniSlimes()
    {

        if (!miniSlimesSpawned)
        {


            GameObject minislime1 = Instantiate(miniSlime, transform.position, Quaternion.identity);
            GameObject minislime2 = Instantiate(miniSlime, transform.position, Quaternion.identity);
            minislime1.GetComponent<HealthSystem>().TurnInvencible(true);
            minislime2.GetComponent<HealthSystem>().TurnInvencible(true);
            CalculateVectors();
            minislime1.GetComponent<Rigidbody2D>().AddForce(vectorSlime1.normalized * spawnForce, ForceMode2D.Impulse);
            minislime2.GetComponent<Rigidbody2D>().AddForce(vectorSlime2.normalized * spawnForce, ForceMode2D.Impulse);

            miniSlimesSpawned = true;
        }
    }


    void CalculateVectors()
    {
        player = GetComponent<EnemyDetectionZone>().player;
        vectorBetweenPlayer = player.transform.position - transform.position;
        vectorSlime1 = new Vector2(-vectorBetweenPlayer.y, vectorBetweenPlayer.x).normalized * spawnForce;
        vectorSlime2 = new Vector2(vectorBetweenPlayer.y, -vectorBetweenPlayer.x).normalized * spawnForce;
    }
}

