using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private const string HIGH_SCORE = "High Score";
    private const string SELECTED_BIRD = "Selected Bird";
    private const string GREEN_BIRD_UNLOCKED = "Green Bird";
    private const string RED_BIRD_UNLOCKED = "Red Bird";

    public int HighScore
    {
        get { return PlayerPrefs.GetInt(HIGH_SCORE); }
        set { PlayerPrefs.SetInt(HIGH_SCORE, value); }
    }
    public int SelectedBird
    {
        get { return PlayerPrefs.GetInt(SELECTED_BIRD); }
        set { PlayerPrefs.SetInt(SELECTED_BIRD, value); }
    }

    private void Awake()
    {
        MakeSingleton();
        IsGameStartedForTheFirstTime();
    }

    private void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    private void IsGameStartedForTheFirstTime()
    {
        if(PlayerPrefs.HasKey("IsTheGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt(SELECTED_BIRD, 0);
            PlayerPrefs.SetInt(GREEN_BIRD_UNLOCKED, 1);
            PlayerPrefs.SetInt(RED_BIRD_UNLOCKED, 1);
            PlayerPrefs.SetInt("IsTheGameStartedForTheFirstTime", 1);
        }
    }

    public void UnlockGreenBird()
    {
        PlayerPrefs.SetInt(GREEN_BIRD_UNLOCKED, 1);
    }

    public int IsGreenBirdUnlocked()
    {
        return PlayerPrefs.GetInt(GREEN_BIRD_UNLOCKED);
    }

    public void UnlockRedBird()
    {
        PlayerPrefs.SetInt(RED_BIRD_UNLOCKED, 1);
    }

    public int IsRedBirdUnlocked()
    {
        return PlayerPrefs.GetInt(RED_BIRD_UNLOCKED);
    }
}
