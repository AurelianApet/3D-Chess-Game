using UnityEngine;
using GoogleMobileAds.Api;
using System;
//TODO: Hashbyte//using admob;
public class admobdemo : MonoBehaviour
{
    public static admobdemo instance;
    //TODO: Hashbyte Admob ad;
    //bool isAdmobInited = false;
    private string appId = "unexpected_platform";

    void Awake()
    {
        instance = this;
#if UNITY_ANDROID
        appId = "ca-app-pub-1511322113455768/3605151532";
#elif UNITY_IPHONE
            appId = "ca-app-pub-3940256099942544~1458002511";        
#endif
        initAdmob();
    }

    void initAdmob()
    {
        MobileAds.Initialize(appId);        
        LoadInterstitial();
        //TODO: Hashbyte
        /*
          //  isAdmobInited = true;
             ad = Admob.Instance();            
            ad.interstitialEventHandler += onInterstitialEvent;            
		ad.initAdmob("ca-app-pub-1511322113455768/6513361448", "ca-app-pub-1511322113455768/3605151532");
        //   ad.setTesting(true);
            ad.setGender(AdmobGender.MALE);
            string[] keywords = { "game","crash","male game"};
            ad.setKeywords(keywords);
            Debug.Log("admob inited -------------");    
        */
    }
    public void LoadInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1511322113455768/3605151532";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        comunication.info.interstitial = new InterstitialAd(adUnitId);
        // Called when an ad request has successfully loaded.
        comunication.info.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        comunication.info.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        comunication.info.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        comunication.info.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        comunication.info.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().AddTestDevice("C9CB63B9A9B2815EDF133511E2D4E2E9").Build();
        comunication.info.interstitial.LoadAd(request);
        //TODO: Hashbyte	ad.loadInterstitial();
    }

    public void ShowInterstitial()
    {
        if (comunication.info.interstitial.IsLoaded())
        {
            comunication.info.interstitial.Show();
        }else{
            Debug.Log("Ad not loaded yet");
        }

        //TODO: Hashbyte
        /*
		if (ad.isInterstitialReady())
		{
			ad.showInterstitial();
		}
		else
		{
			ad.loadInterstitial();
		}
         */
    }

    public void ShowBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1511322113455768/6513361448";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        comunication.info.bannerAd = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        // Called when an ad request has successfully loaded.
        comunication.info.bannerAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        comunication.info.bannerAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        comunication.info.bannerAd.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        comunication.info.bannerAd.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        comunication.info.bannerAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
        .AddTestDevice("C9CB63B9A9B2815EDF133511E2D4E2E9").Build();

        // Load the banner with the request.
        comunication.info.bannerAd.LoadAd(request);


        //TODO: Hashbyte Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.TOP_CENTER, 0);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received ");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
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

    public void HideBanner()
    {
        //TODO: Hashbyte	Admob.Instance ().removeAllBanner ();
    }
    void onInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        //TODO: Hashbyte
        /* 
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
        */
    }
}
