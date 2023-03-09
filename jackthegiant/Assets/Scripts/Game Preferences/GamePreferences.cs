using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePreferences
{
    public static readonly string EasyDifficulty = "EasyDifficulty";
    public static readonly string MediumDifficulty = "MediumDifficulty";
    public static readonly string HardDifficulty = "HardDifficulty";
                  
    public static readonly string EasyDifficultyHighScore = "EasyDifficultHighyScore";
    public static readonly string MediumDifficultyHighScore = "MediumDifficultyHighScore";
    public static readonly string HardDifficultyHighScore = "HardDifficultyHighScore";
                  
    public static readonly string EasyDifficultyCoinScore = "EasyDifficultyCoinScore";
    public static readonly string MediumDifficultyCoinScore = "MediumDifficultyCoinScore";
    public static readonly string HardDifficultyCoinScore = "HardDifficultyCoinScore";
                  
    public static readonly string IsMusicOn = "IsMusicOn";

    public static void SetMusicState(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.IsMusicOn, state);
    }

    public static int GetMusicState()
    {
       return PlayerPrefs.GetInt(GamePreferences.IsMusicOn);
    }

    public static void SetEasyDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(GamePreferences.EasyDifficulty, difficulty);
    }

    public static int GetEasyDifficulty()
    {
        return PlayerPrefs.GetInt(GamePreferences.EasyDifficulty);
    }

    public static void SetMediumDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(GamePreferences.MediumDifficulty, difficulty);
    }

    public static int GetMediumDifficulty()
    {
        return PlayerPrefs.GetInt(GamePreferences.MediumDifficulty);
    }

    public static void SetHardDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(GamePreferences.HardDifficulty, difficulty);
    }

    public static int GetHardDifficulty()
    {
        return PlayerPrefs.GetInt(GamePreferences.HardDifficulty);
    }

    public static void SetEasyDifficultyHighScore(int difficulty)
    {
        PlayerPrefs.SetInt(GamePreferences.EasyDifficultyHighScore, difficulty);
    }

    public static int GetEasyDifficultyHighScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.EasyDifficultyHighScore);
    }

    public static void SetMediumDifficultyHighScore(int difficulty)
    {
        PlayerPrefs.SetInt(GamePreferences.MediumDifficultyHighScore, difficulty);
    }

    public static int GetMediumDifficultyHighScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.MediumDifficultyHighScore);
    }

    public static void SetHardDifficultyHighScore(int difficulty)
    {
        PlayerPrefs.SetInt(GamePreferences.HardDifficultyHighScore, difficulty);
    }

    public static int GetHardDifficultyHighScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.HardDifficultyHighScore);
    }

    public static void SetHardDifficultyCoinScore(int difficulty)
    {
        PlayerPrefs.SetInt(GamePreferences.HardDifficultyCoinScore, difficulty);
    }

    public static int GetHardDifficultyCoinScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.HardDifficultyCoinScore);
    }

    public static void SetMediumDifficultyCoinScore(int difficulty)
    {
        PlayerPrefs.SetInt(GamePreferences.MediumDifficultyCoinScore, difficulty);
    }

    public static int GetMediumDifficultyCoinScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.MediumDifficultyCoinScore);
    }

    public static void SetEasyDifficultyCoinScore(int difficulty)
    {
        PlayerPrefs.SetInt(GamePreferences.EasyDifficultyCoinScore, difficulty);
    }

    public static int GetEasyDifficultyCoinScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.EasyDifficultyCoinScore);
    }
}
