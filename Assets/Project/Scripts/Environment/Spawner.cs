using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public GameObject enemy;
    [SerializeField]
    GameObject player;      

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    public void Spawn()
    {
        GameObject enemyInst = Instantiate(enemy, this.transform.position, Quaternion.identity);       
        Destroy(this);
    }
}
