using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;

public class AdMobScript : MonoBehaviour
{
    public static AdMobScript instance;

    //private readonly string App_ID = "ca-app-pub-5296883921473132~5754997472";

    private readonly string Banner_Ad_ID = "ca-app-pub-3940256099942544/6300978111"; //"ca-app-pub-5296883921473132/3874390857";
    private readonly string Interstitial_Ad_ID = "ca-app-pub-3940256099942544/1033173712"; //"ca-app-pub-5296883921473132/6392742985";
    private readonly string Rewarded_Ad_ID = "ca-app-pub-3940256099942544/5224354917"; //"ca-app-pub-5296883921473132/7359367963";

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewarded;

    private void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
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

    public void RequestBanner()
    {
       
        if (this.bannerView != null)
            this.bannerView.Destroy();
        AdSize customSize = new AdSize(280, 50);

        this.bannerView = new BannerView(Banner_Ad_ID, customSize, AdPosition.Bottom);
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
    }

    public void RequestInterstitial()
    {
        if (this.interstitial != null)
            this.interstitial.Destroy();
        this.interstitial = new InterstitialAd(Interstitial_Ad_ID);


        this.interstitial.OnAdLoaded += this.HandleOnAdLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleOnAdOpened;
        this.interstitial.OnAdClosed += this.HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    public void RequestRewarded()
    {
        if (this.rewarded != null)
            this.rewarded.Destroy();
        this.rewarded = new RewardedAd(Rewarded_Ad_ID);

        this.rewarded.OnAdLoaded += this.HandleOnAdLoaded;
        this.rewarded.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        this.rewarded.OnAdOpening += this.HandleOnAdOpened;
        this.rewarded.OnAdClosed += this.HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewarded.LoadAd(request);
    }

    public void ShowBannerAD()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
            interstitial.Show();
    }

    public void ShowRewardedAd()
    {
        if (rewarded.IsLoaded())
            rewarded.Show();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //adStatus.text = "Ad loaded";
        //MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //adStatus.text = "Ad failed to load";
        //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
}
