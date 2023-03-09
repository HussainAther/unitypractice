using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class MainMenuActions : MonoBehaviour
{
    public void PlayButton()
    {
        VRTK_Logger.Info("Play Button Clicked");
        SceneManager.LoadScene("Room");
    }

    public void QuitButton()
    {
        VRTK_Logger.Info("Quit Button Clicked");
        StartCoroutine(QuitGame());
    }

    private IEnumerator QuitGame()
    {
        yield return null;
        Application.Quit();
    }
}
