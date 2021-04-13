using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    int enemyCount = 0;
    bool spawnsFinished = false;
    bool shouldRepeatLevel = false;

    public void HandleSpawnsComplete(bool shouldRepeat)
    {
        spawnsFinished = true;
        shouldRepeatLevel = shouldRepeat;
    }

    public void HandleEnemySpawned()
    {
        enemyCount++;
    }

    public void HandleEnemyDeath()
    {
        enemyCount--;
        if (spawnsFinished && enemyCount < 1)
        {
            HandleLevelEnd(shouldRepeatLevel);
        }
    }
    
    public void HandleLevelEnd(bool repeat)
    {
        if (repeat)
        {
            FindObjectOfType<Level>().ReloadScene();
        }
        else
        {
            FindObjectOfType<Level>().LoadNextScene();
        }
    }
}
