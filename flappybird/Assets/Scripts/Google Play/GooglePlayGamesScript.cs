using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayGamesScript : MonoBehaviour
{
    public static GooglePlayGamesScript instance;

    private const string leaderboardID = "CgkIgJ-ysY8PEAIQAQ";
    private const string achievementID = "CgkIgJ-ysY8PEAIQAA";
    public static PlayGamesPlatform platform = null;

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

    public void ConnectOrDisconnectOnGoogle()
    {
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.SignOut();
        else
        {
            if (platform == null)
            {
                PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
                PlayGamesPlatform.InitializeInstance(config);
                PlayGamesPlatform.DebugLogEnabled = true;
                platform = PlayGamesPlatform.Activate();
            }

            Social.Active.localUser.Authenticate(success =>
            {
                if (success)
                {
                    Debug.Log("Logged in successfully");
                }
                else
                {
                    Debug.Log("Login Failed");
                }
            });
        }
    }

    public void AddScoreToLeaderboard(int score)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboardID, success => { });
        }
    }

    public void ShowLeaderboard()
    {
        Debug.Log("Showing leaderboard..");
        if (Social.Active.localUser.authenticated)
            platform.ShowLeaderboardUI();
    }

    public void ShowAchievements()
    {
        Debug.Log("Showing achievements..");
        if (Social.Active.localUser.authenticated)
            platform.ShowAchievementsUI();
    }

    public void UnlockAchievement()
    {
        if (Social.Active.localUser.authenticated)
            Social.ReportProgress(achievementID, 100f, success => { });
    }
}