using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace LogicPlatformer
{
    public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] private string _iOsAdUnitId = "Interstitial_iOS";

        private string _adUnitId;
        private int adsLoadCounter = 5;

        public event Action OnCompleteShowdAds;

        public event Action OnADSLoadFailed;

        void Awake()
        {
            // Get the Ad Unit ID for the current platform:
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsAdUnitId
                : _androidAdUnitId;
        }

        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        // Show the loaded content in the Ad Unit:
        public void ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
            LoadAd();
        }

        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
            adsLoadCounter = 5;
        }

        public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
            
            ReloadADS();
        }

        public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.

            OnADSLoadFailed?.Invoke();
        }

        public void OnUnityAdsShowStart(string _adUnitId) { }
        public void OnUnityAdsShowClick(string _adUnitId) 
        {
            Debug.Log("OnUnityAdsShowClick");
        }
        public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
        {
            Debug.Log("OnUnityAdsShowComplete");
            OnCompleteShowdAds?.Invoke();
        }

        private void ReloadADS()
        {
            if (adsLoadCounter > 0)
            {
                adsLoadCounter--;
                LoadAd();
            }
            else
            {
                OnADSLoadFailed?.Invoke();
            }
        }
    }
}
