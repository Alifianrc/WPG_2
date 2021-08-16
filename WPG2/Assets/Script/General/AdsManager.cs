using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    // Ads
    private BannerView bannerAds;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    // Ads Id
    [SerializeField] private string BannerID;
    [SerializeField] private string InterstitialID;
    [SerializeField] private string RewardedID;

    private bool isRewarded;

    // Make this singleton
    public static AdsManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });

        // Prepare rewarded ads
        rewardedAd = new RewardedAd(RewardedID);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    // Banner Ads ----------------------------------------------------------------------
    public void RequestBannerTop()
    {
        bannerAds = new BannerView(BannerID, AdSize.SmartBanner, AdPosition.Top);
        bannerAds.LoadAd(new AdRequest.Builder().Build());
    }
    public void RequestBannerBottom()
    {
        if (bannerAds == null)
        {
            bannerAds = new BannerView(BannerID, AdSize.SmartBanner, AdPosition.Bottom);
            bannerAds.LoadAd(new AdRequest.Builder().Build()); Debug.Log("Banner requested");
        }
    }
    public void DeleteBanner()
    {
        if(bannerAds != null)
        {
            bannerAds.Hide();
            bannerAds.Destroy(); Debug.Log("Banner destroyed");
            bannerAds = null;
        }
    }

    // Interstitial Ads ----------------------------------------------------------------
    public void RequestInterstitial()
    {
        // Cleanup old ads
        if(interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        // Create new one
        interstitialAd = new InterstitialAd(InterstitialID);

        // Load it
        interstitialAd.LoadAd(new AdRequest.Builder().Build());
    }
    public void ShowInterstitial()
    {
        // Check if ads is loaded
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial Ads is not loaded");
        }
    }
    public void ShowInterstitialRandom(int max)
    {
        // Request ads randomly
        if (UnityEngine.Random.Range(0, max) == 0)
        {
            ShowInterstitial();
        }
    }

    // Rewarded Ads --------------------------------------------------------------------
    public void RequestRewarded()
    {
        rewardedAd.LoadAd(new AdRequest.Builder().Build());
    }
    public void ShowRewarded()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }
    private void HandleUserEarnedReward(object sender, Reward args)
    {
        
    }
    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        // Reload maybe?
        RequestRewarded();
    }
}
