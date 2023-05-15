using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace LogicPlatformer
{
    public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        //[SerializeField] Button _showAdButton;
        [SerializeField] string _androidAdUnitId = "Rewarded_Android";
        [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";

        private string _adUnitId = null; // This will remain null for unsupported platforms

        private bool isTakeHint = false;

        private int adsLoadCounter = 5;

        private event Action onStartReward;

        public event Action OnTakeHint;
        public event Action OnOpenNextLevel;
        public event Action OnADSLoadFailed;

        void Awake()
        {
            // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = _androidAdUnitId;
#endif

            // Disable the button until the ad is ready to show:
            //_showAdButton.interactable = false;
        }
        public void ShowRewardVideo(bool isTakeHint)
        {
            this.isTakeHint = isTakeHint;
            onStartReward?.Invoke();
        }
        // Call this public method when you want to get an ad ready to show.
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        // If the ad successfully loads, add a listener to the button and enable it:
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);

            if (adUnitId.Equals(_adUnitId))
            {
                // Configure the button to call the ShowAd() method when clicked:

                //_showAdButton.onClick.AddListener(ShowAd);
                onStartReward += ShowAd;
                // Enable the button for users to click:
                //_showAdButton.interactable = true;
                adsLoadCounter = 5;
            }
        }

        // Implement a method to execute when the user clicks the button:
        public void ShowAd()
        {
            // Disable the button:
            //_showAdButton.interactable = false;
            // Then show the ad:
            Advertisement.Show(_adUnitId, this);
            LoadAd();
        }

        // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                // Grant a reward.
                if (isTakeHint)
                {
                    OnTakeHint?.Invoke();
                }
                else
                {
                    OnOpenNextLevel?.Invoke();
                }
                adsLoadCounter = 5;
            }

        }

        // Implement Load and Show Listener error callbacks:
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.

            ReloadADS();
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

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.

            OnADSLoadFailed?.Invoke();
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { Debug.Log("OnUnityAdsShowClick"); }

        void OnDestroy()
        {
            // Clean up the button listeners:
            //_showAdButton.onClick.RemoveAllListeners();

            onStartReward -= ShowAd;
        }
    }
}