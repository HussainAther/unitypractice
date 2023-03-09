using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    //private Text scoreText, coinText, lifeText;
    [SerializeField]
    private Text scoreText, coinText, lifeText, gameOverScoreText, gameOverCoinText;


    [SerializeField]
    private GameObject pausePanel, gameOverPanel;

    [SerializeField]
    private GameObject readyButton;

    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        Time.timeScale = 0;
        GameObject button = GameObject.FindGameObjectWithTag("PauseButton");
        button.SetActive(true);
        AdMobScript.instance.RequestInterstitial();
    }

    private void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    public void GameOverShowPanel(int finalScore, int finalCoinScore)
    {
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = "" + finalScore;
        gameOverCoinText.text = "" + finalCoinScore;
        StartCoroutine(GameOverLoadMainMenu());
        GameObject button = GameObject.FindGameObjectWithTag("PauseButton");
        button.SetActive(false);
    }

    public void PlayerDiedRestartTheGame()
    {
        StartCoroutine(PlayerDiedRestart());
    }

    private IEnumerator GameOverLoadMainMenu()
    {
        yield return new WaitForSeconds(3f);
        Application.LoadLevel("MainMenu");
    }

    private IEnumerator PlayerDiedRestart()
    {
        yield return new WaitForSeconds(.3f);
        SceneFader.instance.LoadLevel("Gameplay");
    }

    public void SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void SetCoinScore(int coinScore)
    {
        coinText.text = "x" + coinScore;
    }

    public void SetLifeScore(int lifeScore)
    {
        lifeText.text = "x" + lifeScore;
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        //Application.LoadLevel("MainMenu");
        SceneFader.instance.LoadLevel("MainMenu");
    }

    public void StartTheGame()
    {
        Time.timeScale = 1f;
        readyButton.SetActive(false);
    }
}
