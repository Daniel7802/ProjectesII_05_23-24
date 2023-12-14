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
        base.Dead();
        SpawnMiniSlimes();        
        
    }
    void SpawnMiniSlimes()
    {
        
        if(!miniSlimesSpawned)
        {
            CalculateVectors();
            Vector2 vector = transform.position;
            GameObject slime = Instantiate(miniSlime, vector, Quaternion.identity);
            GameObject slime2 = Instantiate(miniSlime, vector, Quaternion.identity);
            slime.GetComponent<SlimeIA>().player = player;
            slime2.GetComponent<SlimeIA>().player = player;
            slime.GetComponent<Enemy>().knockbackForce = 30;
            slime2.GetComponent<Enemy>().knockbackForce = 30;
            slime.GetComponent<Enemy>().KnockBack(vectorSlime1);
            slime2.GetComponent<Enemy>().KnockBack(vectorSlime2);          
            miniSlimesSpawned = true;
        }
    }

    void CalculateVectors()
    {
        vectorBetweenPlayer = player.transform.position - this.transform.position;
        vectorSlime1 = new Vector2(-vectorBetweenPlayer.y, vectorBetweenPlayer.x);
        vectorSlime2 = new Vector2(vectorBetweenPlayer.y, -vectorBetweenPlayer.x);
    }
}

