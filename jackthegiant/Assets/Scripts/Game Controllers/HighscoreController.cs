using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText, coinText;

    void Start()
    {
        SetScoreBasedOnDifficulty();
    }

    private void SetScore(int score, int coinScore)
    {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
    }

    void SetScoreBasedOnDifficulty()
    {
        if(GamePreferences.GetEasyDifficulty() == 1)
        {
            SetScore(GamePreferences.GetEasyDifficultyHighScore(),
                     GamePreferences.GetEasyDifficultyCoinScore());
        }
        else if (GamePreferences.GetMediumDifficulty() == 1)
        {
            SetScore(GamePreferences.GetMediumDifficultyHighScore(),
                     GamePreferences.GetMediumDifficultyCoinScore());
        }
        else if (GamePreferences.GetHardDifficulty() == 1)
        {
            SetScore(GamePreferences.GetHardDifficultyHighScore(),
                     GamePreferences.GetHardDifficultyCoinScore());
        }
    }

    public void GoBack()
    {
        Application.LoadLevel("MainMenu");
    }
}
