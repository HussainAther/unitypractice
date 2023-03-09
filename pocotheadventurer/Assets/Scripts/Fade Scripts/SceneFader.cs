using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    private static SceneFader m_Instance;
    public static SceneFader Instance { get { return m_Instance; } }

    public Animator anim;
    [SerializeField] private GameObject fadePanel;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (m_Instance != null)
            Destroy(gameObject);
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(string newLevelName)
    {
        StartCoroutine(PlayFadeInOutAnimation(newLevelName));
    }

    private IEnumerator PlayFadeInOutAnimation(string newLevelName)
    {
        fadePanel.SetActive(true);
        anim.Play("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(newLevelName);
        anim.Play("FadeOut");
        yield return new WaitForSeconds(1f);

        fadePanel.SetActive(false);
    }
}
