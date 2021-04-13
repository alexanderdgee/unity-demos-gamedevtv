using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int buildCost = 100;
    [SerializeField] int starsGenerated = 0;

    Vector2 spawnLocation;
    DefenderSpawner defenderSpawner;
    StarDisplay starDisplay;

    void Start()
    {
        starDisplay = FindObjectOfType<StarDisplay>();
    }

    private void OnDestroy()
    {
        if (defenderSpawner)
        {
            defenderSpawner.DefenderKilled(spawnLocation);
        }
    }

    public void RegisterSpawnLocation(DefenderSpawner defenderSpawner, Vector2 location)
    {
        this.defenderSpawner = defenderSpawner;
        spawnLocation = location;
    }

    public int GetBuildCost()
    {
        return buildCost;
    }

    public void GenerateStars()
    {
        starDisplay.AddStars(starsGenerated);
    }
}
