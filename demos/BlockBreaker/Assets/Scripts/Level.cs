using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // observers
    [SerializeField] int breakableBlocks;

    GameStatus gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
    }

    public void BlockCreated()
    {
        ++breakableBlocks;
    }

    public void BlockDestroyed()
    {
        --breakableBlocks;
        gameStatus.AddScore();
        if (breakableBlocks < 1)
        {
            gameStatus.LevelComplete();
            FindObjectOfType<SceneLoader>().ReloadScene();
        }
    }
}
