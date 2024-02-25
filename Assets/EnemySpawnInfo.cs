using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    public Transform spawn;
}
[System.Serializable]
public class EnemyWave
{
    public List<EnemySpawnInfo> enemiesToSpawn = new List<EnemySpawnInfo>();
}