using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject entityPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        if (poolSize < 1 || entityPrefab == null)
        {
            poolSize = 0;
        }
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(entityPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEntity());
    }

    IEnumerator SpawnEntity()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
