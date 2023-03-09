using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] 
    public bool gameStatedFromMainMenu, gameRestartedAfterPlayerDied;
    [HideInInspector]
    public int score, coinScore, lifeScore;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        InitializeVariables();
    }
    private void OnLevelWasLoaded(int level) // call each time the scene is changed
    {
        if(Application.loadedLevelName == "Gameplay")
        {
            if(gameRestartedAfterPlayerDied)
            {
                GameplayController.instance.SetScore(score);
                GameplayController.instance.SetCoinScore(coinScore);
                GameplayController.instance.SetLifeScore(lifeScore);

                PlayerScore.scoreCount = score;
                PlayerScore.coinCount = coinScore;
                PlayerScore.lifeCount = lifeScore;
            }
            else if(gameStatedFromMainMenu)
            {
                PlayerScore.scoreCount = 0;
                PlayerScore.coinCount = 0;
                PlayerScore.lifeCount = 2;

                GameplayController.instance.SetScore(0);
                GameplayController.instance.SetCoinScore(0);
                GameplayController.instance.SetLifeScore(2);
            }
        }
    }

    public void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }    
    }

    private void InitializeVariables()
    {
        if(!PlayerPrefs.HasKey("Game Initialized"))
        {
            GamePreferences.SetEasyDifficulty(0);
            GamePreferences.SetEasyDifficultyCoinScore(0);
            GamePreferences.SetEasyDifficultyHighScore(0);

            GamePreferences.SetMediumDifficulty(1);
            GamePreferences.SetMediumDifficultyCoinScore(0);
            GamePreferences.SetMediumDifficultyHighScore(0);

            GamePreferences.SetHardDifficulty(0);
            GamePreferences.SetHardDifficultyCoinScore(0);
            GamePreferences.SetHardDifficultyHighScore(0);
            GamePreferences.SetMusicState(1);

            PlayerPrefs.SetInt("Game Initialized", 1);
        }
    }

    public void CheckGameStatus(int score, int coinScore, int lifeScore)
    {
        if(lifeScore < 0)
        {
            CheckForHighScoreUpdate(score, coinScore);
            gameStatedFromMainMenu = false;
            gameRestartedAfterPlayerDied = false;
            AdMobScript.instance.ShowInterstitial();
            GameplayController.instance.GameOverShowPanel(score, coinScore);
        }
        else
        {
            this.score = score;
            this.coinScore = coinScore;
            this.lifeScore = lifeScore;

            gameRestartedAfterPlayerDied = true;
            gameStatedFromMainMenu = false;

            GameplayController.instance.PlayerDiedRestartTheGame();
        }
    }

    private void CheckForHighScoreUpdate(int score, int coinScore)
    {
        if (GamePreferences.GetEasyDifficulty() == 1)
        {
            int highScore = GamePreferences.GetEasyDifficultyHighScore();
            int coinHighscore = GamePreferences.GetEasyDifficultyCoinScore();

            if (highScore < score)
                GamePreferences.SetEasyDifficultyHighScore(score);

            if (coinHighscore < coinScore)
                GamePreferences.SetEasyDifficultyCoinScore(coinScore);
        }
        else if (GamePreferences.GetMediumDifficulty() == 1)
        {
            int highScore = GamePreferences.GetMediumDifficultyHighScore();
            int coinHighscore = GamePreferences.GetMediumDifficultyCoinScore();

            if (highScore < score)
                GamePreferences.SetMediumDifficultyHighScore(score);

            if (coinHighscore < coinScore)
                GamePreferences.SetMediumDifficultyCoinScore(coinScore);
        }
        else if (GamePreferences.GetHardDifficulty() == 1)
        {
            int highScore = GamePreferences.GetHardDifficultyHighScore();
            int coinHighscore = GamePreferences.GetHardDifficultyCoinScore();

            if (highScore < score)
                GamePreferences.SetHardDifficultyHighScore(score);

            if (coinHighscore < coinScore)
                GamePreferences.SetHardDifficultyCoinScore(coinScore);
        }
    }
}
