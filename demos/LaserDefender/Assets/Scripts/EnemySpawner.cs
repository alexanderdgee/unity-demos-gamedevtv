using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool loop = false;
    [SerializeField] bool randomise = false;

    SpawnManager spawnManager;

    IEnumerator Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        if (randomise)
        {
            waveConfigs = GetComponent<EnemySpawnGenerator>()?.GenerateWaves();
            loop = false;
        }
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (loop);
        spawnManager.HandleSpawnsComplete(randomise);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = 0; i < waveConfigs.Count; i++)
        {
            yield return StartCoroutine(SpawnEnemiesInWave(i));
        }
    }

    private IEnumerator SpawnEnemiesInWave(int waveIndex)
    {
        if (waveIndex < 0 || waveIndex >= waveConfigs.Count)
        {
            yield break;
        }
        var config = waveConfigs[waveIndex];
        for (int i = 0; i < config.GetCount(); i++)
        {
            var newEnemy = Instantiate(config.GetEnemyPrefab(),
                config.GetWaveWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(config);
            spawnManager.HandleEnemySpawned();
            yield return new WaitForSeconds(config.GetSpawnDelay());
        }
    }
}
