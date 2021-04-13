using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName  = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnDelay = 0.5f;
    [SerializeField] float spawnDelayRandom = 0.3f;
    [SerializeField] int count = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public void SetEnemyPrefab(GameObject newEnemy)
    {
        enemyPrefab = newEnemy;
    }

    public List<Transform> GetWaveWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public void SetWaveWaypoints(GameObject newWaypointParent)
    {
        pathPrefab = newWaypointParent;
    }

    public float GetSpawnDelay()
    {
        return spawnDelay;
    }

    public void SetSpawnDelay(float newSpawnDelay)
    {
        spawnDelay = newSpawnDelay;
    }

    public float GetSpawnDelayRandom()
    {
        return spawnDelayRandom;
    }

    public void SetSpawnDelayRandom(float spawnDelayRandomOffset)
    {
        spawnDelayRandom = spawnDelayRandomOffset;
    }

    public int GetCount()
    {
        return count;
    }

    public void SetCount(int count)
    {
        this.count = count;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
