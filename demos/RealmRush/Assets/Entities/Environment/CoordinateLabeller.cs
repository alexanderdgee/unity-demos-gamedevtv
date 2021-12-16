using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeller : MonoBehaviour
{
    [SerializeField] string namePrefix = "";
    [SerializeField] bool rename = true;
    [SerializeField] bool coordinatesStartVisible = true;

    Vector2Int coordinates = new Vector2Int();
    TextMeshPro label;
    TileDebugSettings settings;
    Waypoint waypoint;
    string defaultName = null;

#if UNITY_EDITOR
    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = coordinatesStartVisible; // !Application.isPlaying;
        settings = GetComponent<TileDebugSettings>();
        waypoint = GetComponentInParent<Waypoint>();
        SetLabelColor();
        DisplayCoordinates();
        defaultName = transform.parent.name;
        UpdateObjectName();
    }

    void Update()
    {
        ToggleLabels();
        SetLabelColor();
        if (Application.isPlaying)
        {
            return;
        }
        DisplayCoordinates();
        UpdateObjectName();
    }

    private void SetLabelColor()
    {
        if (!settings)
        {
            return;
        }
        if (!waypoint || waypoint.IsPlaceable)
        {
            label.color = settings.DefaultColor;
        }
        else
        {
            label.color = settings.BlockedColor;
        }
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(
            transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(
            transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }

    private void UpdateObjectName()
    {
        if (rename)
        {
            transform.parent.name = namePrefix + "-" + coordinates.ToString();
        }
        else if (defaultName != null)
        {
            transform.parent.name = defaultName;
        }
    }
#endif
}
