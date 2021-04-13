using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Level time in seconds")]
    [SerializeField] float levelTime = 10;

    bool triggeredLevelFinish = false;
    Slider slider;
    LevelController levelController;

    private void Start()
    {
        slider = GetComponent<Slider>();
        levelController = FindObjectOfType<LevelController>();
    }

    private void Update()
    {
        if (triggeredLevelFinish)
        {
            return;
        }
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        slider.value = Mathf.Clamp(Time.timeSinceLevelLoad / levelTime, 0f, 1f);

        if (Time.timeSinceLevelLoad >= levelTime)
        {
            triggeredLevelFinish = true;
            levelController.LevelTimerFinished();
        }
    }
}
