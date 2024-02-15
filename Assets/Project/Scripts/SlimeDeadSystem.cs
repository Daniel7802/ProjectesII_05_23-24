using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeDeadSystem : DeadSystem
{
    [SerializeField] GameObject miniSlime;
    bool miniSlimesSpawned = false;

    [SerializeField] float spawnForce = 5f;

    public GameObject player;
    private Vector2 vectorBetweenPlayer, vectorSlime1, vectorSlime2;

  



    public override void Dead()
    {
        player = GetComponent<SlimeIA>().player;
        base.Dead();
        SpawnMiniSlimes();
        
    }
    void SpawnMiniSlimes()
    {

        if (!miniSlimesSpawned)
        {
            
            Vector2 vector = transform.position;
            GameObject slime = Instantiate(miniSlime, vector, Quaternion.identity);
            GameObject slime2 = Instantiate(miniSlime, vector, Quaternion.identity);
            slime.GetComponent<SlimeIA>().player = player;
            slime2.GetComponent<SlimeIA>().player = player;

            slime.GetComponent<HealthSystem>().TurnInvencible(true);
            slime2.GetComponent<HealthSystem>().TurnInvencible(true);

            CalculateVectors();
            slime.GetComponent<Rigidbody2D>().AddForce(vectorSlime1,ForceMode2D.Impulse);
            slime2.GetComponent<Rigidbody2D>().AddForce(vectorSlime2, ForceMode2D.Impulse);            

            miniSlimesSpawned = true;
        }
    }


    void CalculateVectors()
    {
        vectorBetweenPlayer = player.transform.position - transform.position;
        vectorSlime1 = new Vector2(-vectorBetweenPlayer.y, vectorBetweenPlayer.x).normalized * spawnForce;
        vectorSlime2 = new Vector2(vectorBetweenPlayer.y, -vectorBetweenPlayer.x).normalized * spawnForce;
    }
}

