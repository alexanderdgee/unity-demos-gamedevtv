using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFFICULTY_KEY = "difficulty";

    const float MIN_VOLUME = 0f;
    public const float DEFAULT_VOLUME = 1f;
    const float MAX_VOLUME = 1f;
    const float MIN_DIFFICULTY = 0f;
    public const float DEFAULT_DIFFICULTY = 1f;
    const float MAX_DIFFICULTY = 3f;

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, DEFAULT_VOLUME);
    }

    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY,
            Mathf.Clamp(volume, MIN_VOLUME, MAX_VOLUME));
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY, DEFAULT_DIFFICULTY);
    }

    public static void SetDifficulty(float difficulty)
    {
        PlayerPrefs.SetFloat(DIFFICULTY_KEY,
            Mathf.Clamp(difficulty, MIN_DIFFICULTY, MAX_DIFFICULTY));
    }
}
