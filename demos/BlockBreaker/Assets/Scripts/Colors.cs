using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public static readonly Color32 RED = new Color32(255, 0, 0, 255);
    public static readonly Color32 ORANGE = new Color32(255, 140, 0, 255);
    public static readonly Color32 YELLOW = new Color32(255, 255, 0, 255);
    public static readonly Color32 GREEN = new Color32(0, 255, 0, 255);
    public static readonly Color32 CYAN = new Color32(0, 255, 255, 255);
    public static readonly Color32 WHITE = new Color32(255, 255, 255, 255);
    public static readonly Color32[] COLORS = { WHITE, CYAN, GREEN, YELLOW, ORANGE, RED };
}
