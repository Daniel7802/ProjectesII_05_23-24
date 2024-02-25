using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{   
    public GameObject spawnAlert; // Prefab del signo de alerta

    public List<EnemyWave> waves = new List<EnemyWave>();
    private int currentWaveIndex = -1;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    public bool isFinished = false; 


    private void Start()
    {
        StartCoroutine(StartNextWaveWithDelay());
    }    
    
    private IEnumerator StartNextWaveWithDelay()
    {
        
        while (true)
        {
            yield return new WaitUntil(() => AreAllWaveEnemiesDead());

            // Lista para almacenar las instancias de los signos de alerta
            List<GameObject> alertSigns = new List<GameObject>();

            if (currentWaveIndex + 1 < waves.Count) // Verificar si hay m�s oleadas
            {
                // Instanciar un signo de alerta en la posici�n de cada enemigo de la pr�xima oleada
                foreach (var enemyInfo in waves[currentWaveIndex + 1].enemiesToSpawn)
                {
                    GameObject alertSign = Instantiate(spawnAlert, enemyInfo.spawn.position, Quaternion.identity);

                    alertSigns.Add(alertSign); // A�adir a la lista
                }

                yield return new WaitForSeconds(2f); // Esperar 2 segundos con los signos de alerta activos

                // Desactivar y destruir los signos de alerta
                foreach (var sign in alertSigns)
                {
                    Destroy(sign);
                }
            }

            yield return new WaitForSeconds(0.5f); // Tiempo adicional de espera si es necesario
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex < waves.Count)
        {
            spawnedEnemies.Clear();
            SpawnWave(waves[currentWaveIndex]);
        }
        else
        {
            isFinished = true;
            Debug.Log("Todas las oleadas completadas.");
        }
    }

    private void SpawnWave(EnemyWave wave)
    {
        foreach (var enemyInfo in wave.enemiesToSpawn)
        {
            GameObject spawnedEnemy = Instantiate(enemyInfo.enemyPrefab, enemyInfo.spawn.position, Quaternion.identity);           
            spawnedEnemies.Add(spawnedEnemy);
        }
    }

    private bool AreAllWaveEnemiesDead()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null);
        return spawnedEnemies.Count == 0;
    }
   
}