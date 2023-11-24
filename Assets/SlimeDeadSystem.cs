using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeadSystem : DeadSystem
{
    [SerializeField] GameObject miniSlime;
    bool miniSlimesSpawned = false;
    public GameObject player;
    private Vector3 vectorBetweenPlayer, vectorSlime1, vectorSlime2;

    public override void Dead()
    {
        SpawnMiniSlimes();
        base.Dead();
    }
    void SpawnMiniSlimes()
    {
        //Vector2 dir = new Vector2(0, 3);
        if(!miniSlimesSpawned)
        {
            CalculateVectors();
            Vector2 vector = transform.position;
            GameObject slime = Instantiate(miniSlime, vector, Quaternion.identity);
            GameObject slime2 = Instantiate(miniSlime, vector, Quaternion.identity);
            slime.GetComponent<SlimeIA>().player = player;
            slime2.GetComponent<SlimeIA>().player = player;
            slime.GetComponent<Enemy>().knockbackForce = 10;
            slime2.GetComponent<Enemy>().knockbackForce = 10;
            slime.GetComponent<Enemy>().KnockBack(vectorSlime1);
            slime2.GetComponent<Enemy>().KnockBack(vectorSlime2);

            slime.GetComponent<Enemy>().knockbackForce = 5;
            slime2.GetComponent<Enemy>().knockbackForce = 5;
            miniSlimesSpawned = true;
        }
       

       
       // Vector2 spawnForce = dir.normalized * miniSlimeSpawnForce;
       // rb2D.AddForce(spawnForce, ForceMode2D.Impulse);            

       // dir = new Vector2(0, -3);
       // GameObject slime2 = Instantiate(miniSlime);
       // Vector2 spawnForce2 = dir.normalized * miniSlimeSpawnForce;
       //rb2D.AddForce(spawnForce2, ForceMode2D.Impulse);

    }

    void CalculateVectors()
    {
        vectorBetweenPlayer = player.transform.position - this.transform.position;
        vectorSlime1 = new Vector2(-vectorBetweenPlayer.y, vectorBetweenPlayer.x);
        vectorSlime2 = new Vector2(vectorBetweenPlayer.y, -vectorBetweenPlayer.x);
    }
}

