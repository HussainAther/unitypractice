using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button musicButton;

    [SerializeField]
    private Sprite[] musicIcons;


    void Start()
    {
        CheckToPlayTheMusic();
        AdMobScript.instance.RequestBanner();
        AdMobScript.instance.ShowBannerAD();
    }

    private void CheckToPlayTheMusic()
    {
        if(GamePreferences.GetMusicState() == 1)
        {
            MusicController.instance.PlayMusic(true);
            musicButton.image.sprite = musicIcons[0];
        }
        else
        {
            MusicController.instance.PlayMusic(false);
            musicButton.image.sprite = musicIcons[1];
        }
    }

    public void StartGame()
    {
        GameManager.instance.gameStatedFromMainMenu = true;
        //Application.LoadLevel("Gameplay");
        SceneFader.instance.LoadLevel("Gameplay");
    }

    public void HighscoreMenu()
    {
        Application.LoadLevel("HighScoreScene");
    }

    public void OptionsMenu()
    {
        Application.LoadLevel("OptionsMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MusicButton()
    {
        if (GamePreferences.GetMusicState() == 0)
        {
            GamePreferences.SetMusicState(1);
            MusicController.instance.PlayMusic(true);
            musicButton.image.sprite = musicIcons[0];
        } 
        else if (GamePreferences.GetMusicState() == 1)
        {
            GamePreferences.SetMusicState(0);
            MusicController.instance.PlayMusic(false);
            musicButton.image.sprite = musicIcons[1];
        }
    }
}
