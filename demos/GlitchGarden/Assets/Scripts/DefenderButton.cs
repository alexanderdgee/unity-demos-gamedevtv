using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    static Color gray = new Color32(50, 50, 50, 255);
    
    [SerializeField] Defender defenderPrefab;

    new SpriteRenderer renderer;
    DefenderSpawner spawner;
    DefenderButton[] allDefenders;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = gray;
        spawner = FindObjectOfType<DefenderSpawner>();
        allDefenders = FindObjectsOfType<DefenderButton>();
        LabelButtonWithCost();
    }

    private void LabelButtonWithCost()
    {
        Text costText = GetComponentInChildren<Text>();
        if (!costText)
        {
            Debug.LogError(name + " has no cost text!");
        }
        else
        {
            costText.text = defenderPrefab.GetBuildCost().ToString();
        }
    }

    private void OnMouseDown()
    {
        foreach (var button in allDefenders)
        {
            button.SetColor(gray);
        }
        SetColor(Color.white);
        spawner.SetDefender(defenderPrefab);
    }

    public void SetColor(Color32 newColor)
    {
        renderer.color = newColor;
    }
}
