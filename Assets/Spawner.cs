using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    GameObject player;
      

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        GameObject enemyInst = Instantiate(enemy, this.transform.position, Quaternion.identity);
        if (enemyInst == enemyInst.GetComponent<SlimeIA>()) 
        {
            enemyInst.GetComponent<SlimeIA>().player = player;
        }
        if (enemyInst == enemyInst.GetComponent<ShootyIA>())
        {
            enemyInst.GetComponent<SlimeIA>().player = player;
        }
    }
}
