using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private GameObject easySign, mediumSign, hardSign;

    void Start()
    {
        SetTheDifficulty();
    }

    void SetInitialDifficulty(string difficulty)
    {
        switch(difficulty)
        {
            case "easy": 
                easySign.SetActive(true);
                mediumSign.SetActive(false);
                hardSign.SetActive(false);
                break;
            case "medium":
                easySign.SetActive(false);
                mediumSign.SetActive(true);
                hardSign.SetActive(false);
                break;
            case "hard":
                easySign.SetActive(false);
                mediumSign.SetActive(false);
                hardSign.SetActive(true);
                break;
        }
    }

    private void SetTheDifficulty()
    {
        if(GamePreferences.GetEasyDifficulty() == 1)
            SetInitialDifficulty("easy");
        else if (GamePreferences.GetMediumDifficulty() == 1)
            SetInitialDifficulty("medium");
        else if (GamePreferences.GetHardDifficulty() == 1)
            SetInitialDifficulty("hard");
    }

    public void EasyDifficulty()
    {
        GamePreferences.SetEasyDifficulty(1);
        GamePreferences.SetMediumDifficulty(0);
        GamePreferences.SetHardDifficulty(0);
        SetInitialDifficulty("easy");
    }

    public void MediumDifficulty()
    {
        GamePreferences.SetEasyDifficulty(0);
        GamePreferences.SetMediumDifficulty(1);
        GamePreferences.SetHardDifficulty(0);
        SetInitialDifficulty("medium");
    }

    public void HardDifficulty()
    {
        GamePreferences.SetEasyDifficulty(0);
        GamePreferences.SetMediumDifficulty(0);
        GamePreferences.SetHardDifficulty(1);
        SetInitialDifficulty("hard");
    }
    public void GoBack()
    {
        Application.LoadLevel("MainMenu");
    }
}
