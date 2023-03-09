using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Animator soundAnimator;

    public void StartTheGame()
    {
        soundAnimator.Play("FadeOut");
        SceneFader.Instance.LoadLevel("Level1");
    }
}
