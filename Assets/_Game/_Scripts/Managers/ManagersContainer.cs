using LogicPlatformer.Data;
using LogicPlatformer.Player;
using LogicPlatformer.UI;
using UnityEngine;

namespace LogicPlatformer
{
    public class ManagersContainer : MonoBehaviour
    {
        [SerializeField] private MainUI mainUI;

        [SerializeField] private IDataManager dataManager;

        [SerializeField] private GamePlayManager gamePlayManager;

        [SerializeField] private IPlayerProfileManager playerProfileManager;

        [SerializeField] private SettingsManager settingsManager;

        [SerializeField] private AudioManager audioManager;

        public MainUI GetMainUI => mainUI;

        public IDataManager GetDataManager => dataManager;

        public IPlayerProfileManager GetPlayerProfileManager => playerProfileManager;

        public SettingsManager GetSettingsManager => settingsManager;

        public GamePlayManager GetGamePlayManager => gamePlayManager;

        public AudioManager GetAudioManager => audioManager;

    }
}
