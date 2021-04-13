using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    static Color green = new Color32(47, 222, 0, 255);
    static Color amber = new Color32(222, 139, 0, 255);
    static Color red = new Color32(255, 35, 46, 255);

    [SerializeField] Slider difficultySlider;
    [SerializeField] Image difficultyFill;
    [SerializeField] Slider volumeSlider;

    MusicPlayer musicPlayer;
    LevelLoader levelLoader;

    private void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        levelLoader = FindObjectOfType<LevelLoader>();
        difficultySlider.value = PlayerPrefsController.GetDifficulty();
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
    }

    private void Update()
    {
        if (musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
        if (difficultyFill)
        {
            switch (difficultySlider.value)
            {
                case 2f:
                    difficultyFill.color = amber;
                    break;
                case 3f:
                    difficultyFill.color = red;
                    break;
                default:
                    difficultyFill.color = green;
                    break;
            }
        }
    }

    public void ResetToDefaults()
    {
        difficultySlider.value = PlayerPrefsController.DEFAULT_DIFFICULTY;
        volumeSlider.value = PlayerPrefsController.DEFAULT_VOLUME;
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetDifficulty(difficultySlider.value);
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        levelLoader.LoadStartScreen();
    }
}
