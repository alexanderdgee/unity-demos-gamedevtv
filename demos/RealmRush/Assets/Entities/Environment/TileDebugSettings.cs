using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDebugSettings : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.blue;

    public Color DefaultColor { get => defaultColor; }
    public Color BlockedColor { get => blockedColor; }
}
