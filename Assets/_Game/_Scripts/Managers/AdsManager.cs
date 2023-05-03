using UnityEngine;
using UnityEngine.Advertisements;

namespace LogicPlatformer
{
    public class AdsManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsInitializationListener, IUnityAdsLoadListener
    {
        [SerializeField] private bool testMode = true;

        private string gameID = "5260561"; // myAdUnitId

        public string adUnitIdAndroid = "Interstitial_Android";

        public string rewardUnitIdAndroid = "Rewarded_Android";

        public bool adStarted;

        public bool adCompleted;

        public string myAdStatus = "";

        public void Initialize()
        {
            Advertisement.Initialize(gameID, testMode);
        }

        public void ShowAdsVideo()
        {
            if (Advertisement.isInitialized && !adStarted)
            {
                Advertisement.Load(adUnitIdAndroid);
                Advertisement.Show(adUnitIdAndroid);
                adStarted = true;
            }
            else
            {
                Debug.Log("Not Ads Video");
            }
        }

        public void ShowRevardedVideo()
        {
            if (Advertisement.isInitialized && !adStarted)
            {
                Advertisement.Show(rewardUnitIdAndroid);
            }
            else
            {
                Debug.Log("Not Revarded Video");
            }
        }

        // -------   IUnityAdsInitializationListener   ------
        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            Advertisement.Load(rewardUnitIdAndroid, this);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            myAdStatus = message;
            Debug.Log($"Unity Rewarded Ads Initialization Failed: {error.ToString()} - {message}");
        }

        // -------   IUnityAdsLoadListener    ----------
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);

            if (!adStarted)
            {
                Advertisement.Show(rewardUnitIdAndroid, this);
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            myAdStatus = message;
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        //--------   IUnityAdsShowListener   ---------
        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            myAdStatus = message;
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            adStarted = true;
            Debug.Log("Ad Started: " + adUnitId);
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
            Debug.Log("Ad Clicked: " + adUnitId);
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            adCompleted = showCompletionState == UnityAdsShowCompletionState.COMPLETED;
            Debug.Log("Ad Completed: " + adUnitId);
        }
    }
}
