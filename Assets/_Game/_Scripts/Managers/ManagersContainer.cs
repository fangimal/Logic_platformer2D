using LogicPlatformer.Data;
using LogicPlatformer.Player;
using LogicPlatformer.UI;
using UnityEngine;

namespace LogicPlatformer
{
    public class ManagersContainer : MonoBehaviour
    {
        [SerializeField] private MainUI mainUI;

        [SerializeField] private DataManager dataManager;

        [SerializeField] private GamePlayManager gamePlayManager;

        [SerializeField] private PlayerProfileManager playerProfileManager;

        [SerializeField] private SettingsManager settingsManager;

        [SerializeField] private AnalyticsManager analytics;

        //[SerializeField] private AudioManager audioManager;

        public MainUI GetMainUI => mainUI;

        public DataManager GetDataManager => dataManager;

        public PlayerProfileManager GetPlayerProfileManager => playerProfileManager;

        public SettingsManager GetSettingsManager => settingsManager;

        public GamePlayManager GetGamePlayManager => gamePlayManager;

        public AnalyticsManager GetAnalyticsManager => analytics;

        //public AudioManager GetAudioManager => audioManager;

    }
}
