﻿using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Quiz.Framework.Ad.AdMob
{
    public class AdMobBanner : SingletonMonoBehaviour<MonoBehaviour> {

        private BannerView bannerView;

        private readonly bool testMode = AdMobConstants.IsAdMobTestMode;

        [SerializeField] private string iOSAdUnitId = "ca-app-pub-2837388897714947/5050002152"; 
        [SerializeField]
        private string AndroidAdUnitId = "ca-app-pub-2837388897714947/4438121778";
        public void Start()
        {
            #if UNITY_EDITOR
            #else
            #endif
            this.RequestBanner();
        }

        string GetAdUnitId()
        {
            string adUnitId = "unexpected_platform";
            if (testMode)
            {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/6300978111"; // test
#elif UNITY_IPHONE
                adUnitId = "ca-app-pub-3940256099942544/2934735716"; // test
#endif
            }
            else
            {
#if UNITY_ANDROID
            adUnitId = AndroidAdUnitId;
#elif UNITY_IPHONE
                adUnitId = iOSAdUnitId;
#endif
            }

            return adUnitId;
        }

        public void RequestBanner()
        {
            string adUnitId = GetAdUnitId();
        
            AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
            bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
        
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            bannerView.LoadAd(request);
        }
    

        public void Hide()
        {
            if (bannerView != null)
            {
                bannerView.Hide();
            //    bannerView.Destroy();
            //    bannerView = null;
            }
        }

        public void Show()
        {
            if (bannerView != null)
            {
                bannerView.Show();
            }
            #if false
            else
            {
                RequestBanner();
            }
            #endif
        }
    }
}

