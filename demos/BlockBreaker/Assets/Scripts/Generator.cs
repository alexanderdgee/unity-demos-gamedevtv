using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    const string BLOCKS_PARENT = "blocks";

    [SerializeField] Block breakablePrefab;
    [SerializeField] Block unbreakablePrefab;
    [SerializeField] float minX = -7;
    [SerializeField] float maxX = 7;
    [SerializeField] float minY = 0;
    [SerializeField] float maxY = 8;
    [SerializeField] float sizeX = 0.5f;
    [SerializeField] float sizeY = 0.5f;
    [SerializeField] float skipChance = 0.2f;
    [SerializeField] float unbreakableChance = 0.02f;
    [SerializeField] int maxSize = 3;
    [SerializeField] float sizeChance = 0.05f;
    [SerializeField] int maxDurability = 6;
    [SerializeField] float durabilityChance = 0.1f;

    private void Awake()
    {
        if (!breakablePrefab) { return; }
        GameObject parent = GameObject.Find(BLOCKS_PARENT);
        if (!parent)
        {
            parent = new GameObject(BLOCKS_PARENT);
        }
        var maxHori = Mathf.Floor((maxX - minX) / sizeX);
        var maxVert = Mathf.Floor((maxY - minY) / sizeY);
        var actualHori = Mathf.RoundToInt(Random.Range(1, maxHori));
        var actualVert = Mathf.RoundToInt(Random.Range(1, maxVert));
        var midpoint = new Vector2(actualHori, actualVert) / 2f;
        bool[,] grid = new bool[actualHori, actualVert];
        for (var i = 0; i < actualHori; i++)
        {
            for (var j = 0; j < actualVert; j++)
            {
                if (grid[i,j]) { continue; }
                if (Random.value < skipChance)
                {
                    grid[i,j] = true;
                    continue;
                }
                CreateBlock(parent, midpoint, grid, i, j);
            }
        }
    }

    private void CreateBlock(GameObject parent, Vector2 midpoint, bool[,] grid,
        int i, int j)
    {
        var (prefab, useUnbreakable) = ChooseIfBreakable();
        var blockSize = ChooseSize(grid, i, j);
        var offset = GetOffset(blockSize);
        var block = Instantiate(prefab,
            GetPosition(midpoint, i, j, offset), Quaternion.identity);
        block.transform.localScale = new Vector3(blockSize, blockSize, 1);
        block.transform.SetParent(parent.transform);
        CustomiseNewBlock(useUnbreakable, block);
        PreventOverlaps(grid, i, j, blockSize);
    }

    private (Block, bool) ChooseIfBreakable()
    {
        var useUnbreakable = false;
        var prefab = breakablePrefab;
        if (Random.value < unbreakableChance)
        {
            useUnbreakable = true;
            prefab = unbreakablePrefab;
        }
        return (prefab, useUnbreakable);
    }

    private int ChooseSize(bool[,] grid, int i, int j)
    {
        var sizeChoice = Random.value;
        for (var s = maxSize; s > 1; s--)
        {
            if (sizeChoice < Mathf.Pow(sizeChance, s - 1)
                && CanPlaceBlock(grid, i, j, s))
            {
                return s;
            }
        }
        return 1;
    }

    private float GetOffset(int blockSize)
    {
        return (Mathf.Max(1f, blockSize) - 1) / 2f;
    }

    private Vector3 GetPosition(Vector2 midpoint, int i, int j, float offset)
    {
        return new Vector3(
            (i - midpoint.x + offset) * sizeX,
            (j - midpoint.y + offset) * sizeY,
            0) + transform.position;
    }

    private void CustomiseNewBlock(bool useUnbreakable, Block block)
    {
        if (!useUnbreakable)
        {
            var durability = ChooseDurability();
            block.SetDurability(durability);
            block.SetColor(Colors.COLORS[durability - 1]);
        }
    }

    private int ChooseDurability()
    {
        var durabilityChoice = Random.value;
        for (var i = maxDurability; i > 1; i--)
        {
            if (durabilityChoice < Mathf.Pow(durabilityChance, i - 1))
            {
                return i;
            }
        }
        return 1;
    }

    private static bool CanPlaceBlock(bool[,] grid,
        int i, int j, int blockSize)
    {
        if (i + blockSize >= grid.GetLength(0)
            || j + blockSize >= grid.GetLength(1))
        {
            return false;
        }
        if (grid[i, j]) { return false; }
        if (blockSize > 1
            && (grid[i, j + 1]
            || grid[i + 1, j]
            || grid[i + 1, j + 1]))
        {
            return false;
        }
        if (blockSize > 2
            && (grid[i, j + 2]
            || grid[i + 1, j + 2]
            || grid[i + 2, j]
            || grid[i + 2, j + 1]
            || grid[i + 2, j + 2]))
        {
            return false;
        }
        return true;
    }

    private static void PreventOverlaps(bool[,] grid,
        int i, int j, int blockSize)
    {
        grid[i, j] = true;
        if (blockSize > 1)
        {
            grid[i, j + 1] = true;
            grid[i + 1, j] = true;
            grid[i + 1, j + 1] = true;
        }
        if (blockSize > 2)
        {
            grid[i, j + 2] = true;
            grid[i + 1, j + 2] = true;
            grid[i + 2, j] = true;
            grid[i + 2, j + 1] = true;
            grid[i + 2, j + 2] = true;
        }
    }
}
