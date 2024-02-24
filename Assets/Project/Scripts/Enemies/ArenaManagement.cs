using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArenaManagement : MonoBehaviour
{
    
    [SerializeField] public List<List<Spawner>> waves;

    [SerializeField] private List<Spawner> wave1;
    [SerializeField] private List<Spawner> wave2;
    [SerializeField] private List<Spawner> wave3;
    [SerializeField] private List<Spawner> wave4;
    [SerializeField] private List<Spawner> wave5;

    [SerializeField] private List<GameObject> enemiesInWave;

    bool enemiesAdded = false;
   
   

    int actualWaveIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        waves = new List<List<Spawner>>();
        //wave1 = new List<Spawner>();
        //wave2 = new List<Spawner>();
        enemiesInWave = new List<GameObject>();
        waves.Add(wave1);
        waves.Add(wave2);
        
       //waves.Add(wave3);
    }

    // Update is called once per frame
    void Update()
    {
        StartNextWave();
        if (!enemiesAdded)
        {
            foreach (Spawner spawner in waves[actualWaveIndex])
            {
                enemiesInWave.Add(spawner.enemy);
            }
            enemiesAdded=true;
        }
        
        if(enemiesInWave.Count<=0) {
            actualWaveIndex++;
            enemiesAdded = false;
        }
    }
    public void StartNextWave()
    {
        if (actualWaveIndex < waves.Count&&!enemiesInWave.Any())
        {
            SpawnWave(waves[actualWaveIndex]);

        }
        else
        {
            Debug.Log("sacabo");
        }
    }
    private void SpawnWave(List<Spawner> level)
    {
        foreach (Spawner spawner in level)
        {
            spawner.Spawn();
        }
    }
}

