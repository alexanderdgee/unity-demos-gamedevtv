using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] float initialDelay = 1f;
    [SerializeField] float minSpawnDelay = 2f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Attacker[] attackerPrefabs;

    bool spawn = true;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(initialDelay);
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnAttacker()
    {
        if (attackerPrefabs.Length < 1)
        {
            return;
        }
        var attackerChoice = Random.Range(0, attackerPrefabs.Length);
        Spawn(attackerPrefabs[attackerChoice]);
    }

    private void Spawn(Attacker attacker)
    {
        var newAttacker = Instantiate(attacker,
            transform.position, transform.rotation) as Attacker;
        newAttacker.transform.parent = transform;
    }

    public bool AttackerInLane()
    {
        return transform.childCount > 0;
    }
}
