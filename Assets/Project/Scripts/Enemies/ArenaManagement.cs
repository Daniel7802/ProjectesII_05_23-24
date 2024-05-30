using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    public bool restart = false;
    public GameObject spawnAlert;

    public List<EnemyWave> waves = new List<EnemyWave>();
    private int currentWaveIndex = -1;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    public bool isFinished = false;

    private void Start()
    {
        StartCoroutine(StartNextWaveWithDelay());
    }
    private void Update()
    {
        restartButton.onClick.AddListener(ResetArena);
    }

    private IEnumerator StartNextWaveWithDelay()
    {
        while (true)
        {

            yield return new WaitUntil(() => AllWaveEnemiesDead());

            List<GameObject> alertSigns = new List<GameObject>();

            if (currentWaveIndex + 1 < waves.Count)
            {
                foreach (var enemyInfo in waves[currentWaveIndex + 1].enemiesToSpawn)
                {
                    GameObject alertSign = Instantiate(spawnAlert, enemyInfo.spawn.position, Quaternion.identity);
                    alertSigns.Add(alertSign);
                }

                yield return new WaitForSeconds(2f);

                foreach (var sign in alertSigns)
                {
                    Destroy(sign);
                }
            }

            yield return new WaitForSeconds(0f);
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
        }
    }

    private void SpawnWave(EnemyWave wave)
    {
        foreach (var enemyInfo in wave.enemiesToSpawn)
        {
            GameObject spawnedEnemy = Instantiate(enemyInfo.enemyPrefab, enemyInfo.spawn.position, Quaternion.identity);
            if (!spawnedEnemy.GetComponent<DestructionObjectDeadSystem>())
            {
                spawnedEnemies.Add(spawnedEnemy);
            }
        }
    }

    private bool AllWaveEnemiesDead()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null);
        return spawnedEnemies.Count == 0;
    }

    void ResetArena()
    {
        restart = true;
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }
        spawnedEnemies.Clear();
        currentWaveIndex = -1;
        isFinished = false;
        StartCoroutine(StartNextWaveWithDelay());
    }
}