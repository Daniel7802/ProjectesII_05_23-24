using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeadSystem : DeadSystem
{
    [SerializeField] GameObject miniSlime;
    bool miniSlimesSpawned = false;
    public GameObject player;



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
            GameObject slime = Instantiate(miniSlime);
            slime.transform.position = transform.position;
            slime.GetComponent<SlimeIA>().player = player;
            miniSlimesSpawned = true;
        }
       

       
        //Vector2 spawnForce = dir.normalized * miniSlimeSpawnForce;
        //rb2D.AddForce(spawnForce, ForceMode2D.Impulse);            

        //dir = new Vector2(0, -3);
        //GameObject slime2 = Instantiate(miniSlime);
        //slime2.transform.position = transform.position;
        //Vector2 spawnForce2 = dir.normalized * miniSlimeSpawnForce;
        //rb2D.AddForce(spawnForce2, ForceMode2D.Impulse);

    }
}

