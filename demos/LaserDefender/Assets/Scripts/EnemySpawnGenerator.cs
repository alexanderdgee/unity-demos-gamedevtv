using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnGenerator : MonoBehaviour
{
    const string ROOT = "GeneratedObjects";

    [SerializeField] List<Enemy> enemyPrefabs;
    [SerializeField] Rect leftRect;
    [SerializeField] Rect rightRect;
    [SerializeField] Rect topRect;
    [SerializeField] Rect centreRect;
    [SerializeField] int minWaves = 3;
    [SerializeField] int maxWaves = 10;
    [SerializeField] int minEnemiesPerWave = 3;
    [SerializeField] int maxEnemiesPerWave = 10;

    GameObject root;

    public List<WaveConfig> GenerateWaves()
    {
        var waves = new List<WaveConfig>();
        if (!CanGenerate()) { return waves; }
        root = GameObject.Find(ROOT);
        if (!root) { root = new GameObject("ROOT"); }
        var waveCount = Choice(minWaves, maxWaves + 1);
        for (var i = 0; i < waveCount; i++)
        {
            var wave = ScriptableObject.CreateInstance<WaveConfig>();
            var enemy = enemyPrefabs[Choice(0, enemyPrefabs.Count)];
            wave.SetEnemyPrefab(enemy.gameObject);
            wave.SetMoveSpeed(enemy.GetDefaultSpeed());
            wave.SetCount(Choice(minEnemiesPerWave, maxEnemiesPerWave));
            wave.SetWaveWaypoints(GeneratePath());
            waves.Add(wave);
        }
        return waves;
    }

    bool CanGenerate()
    {
        return enemyPrefabs.Count > 0
            && leftRect != null
            && rightRect != null
            && topRect != null
            && centreRect != null;
    }

    int Choice(int min, int max)
    {
        // min is inclusive, max is exclusive
        return Mathf.FloorToInt(Random.Range(min, max));
    }

    int Choice(int max)
    {
        return Choice(0, max);
    }

    GameObject GeneratePath()
    {
        var parent = new GameObject("path");
        parent.transform.SetParent(root.transform, true);
        List<Rect> terminalChoices = new List<Rect> { leftRect, topRect, rightRect };
        Rect startBounds = terminalChoices[Choice(terminalChoices.Count)];
        terminalChoices.Remove(startBounds);
        Rect endBounds = terminalChoices[Choice(terminalChoices.Count)];
        GenerateWaypoint(startBounds).transform.SetParent(parent.transform, true);
        GenerateWaypoint(centreRect).transform.SetParent(parent.transform, true);
        GenerateWaypoint(endBounds).transform.SetParent(parent.transform, true);
        return parent;
    }

    GameObject GenerateWaypoint(Rect bounds)
    {
        var waypoint = new GameObject("waypoint");
        waypoint.transform.position = new Vector2(
            Random.Range(bounds.x, bounds.x + bounds.width),
            Random.Range(bounds.y, bounds.y + bounds.height)
        );
        return waypoint;
    }
}
