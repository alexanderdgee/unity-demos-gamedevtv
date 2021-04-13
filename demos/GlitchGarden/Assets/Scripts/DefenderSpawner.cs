using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    const string DEFENDER_PARENT_NAME = "Defenders";

    StarDisplay starDisplay;
    Defender defenderTemplate;
    GameObject defenderParent;

    Dictionary<Vector2, Defender> spawnedDefenders = new Dictionary<Vector2, Defender>();

    private void Start()
    {
        starDisplay = FindObjectOfType<StarDisplay>();
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    public void SetDefender(Defender newDefender)
    {
        defenderTemplate = newDefender;
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefender();
    }

    private void AttemptToPlaceDefender()
    {
        if (!starDisplay || !defenderTemplate)
        {
            return;
        }
        var loc = GridCell(PointClicked());
        if (!spawnedDefenders.ContainsKey(loc)
            && starDisplay
            && starDisplay.SpendStars(defenderTemplate.GetBuildCost())) {
            SpawnDefender(loc);
        }
    }

    private Vector2 PointClicked()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector2 GridCell(Vector2 point)
    {
        return new Vector2(Mathf.Round(point.x), Mathf.Round(point.y));
    }

    private void SpawnDefender(Vector2 loc)
    {
        if (defenderTemplate) {
            var defender = Instantiate(defenderTemplate,
                loc, Quaternion.identity) as Defender;
            defender.transform.parent = defenderParent.transform;
            spawnedDefenders.Add(loc, defender);
            defender.RegisterSpawnLocation(this, loc);
        }
    }

    public void DefenderKilled(Vector2 loc)
    {
        spawnedDefenders.Remove(loc);
    }
}
