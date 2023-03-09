using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;

    [SerializeField] private GameObject fadeCanvas;
    [SerializeField] private Animator fadeAnim;

    private void Awake()
    {
        MakeSingleton();
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

    public void FadeInAndOut(string levelName)
    {
        StartCoroutine(FadeInOutAnimation(levelName));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutAnimation());
    }

    IEnumerator FadeInOutAnimation(string levelName)
    {
        fadeCanvas.SetActive(true);
        fadeAnim.Play("FadeIn");
        yield return StartCoroutine(FadeHelpCoroutine.WaitForRealSeconds(.7f));
        Application.LoadLevel(levelName);
        FadeOut();
    }

    IEnumerator FadeOutAnimation()
    {
        fadeAnim.Play("FadeOut");
        yield return StartCoroutine(FadeHelpCoroutine.WaitForRealSeconds(1f));
        fadeCanvas.SetActive(false);
    }
}
