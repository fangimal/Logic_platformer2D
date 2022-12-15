using LogicPlatformer.UI;
using LogicPlatformer.Level;
using UnityEngine;
using LogicPlatformer.Data;
using LogicPlatformer.Player;
using LogicPlatformer.Settings;
using LogicPlatformer.GamePlay;

namespace LogicPlatformer
{
    public class ManagersContainer : MonoBehaviour
    {
        [SerializeField] private MainUI mainUI;

        [SerializeField] private IDataManager dataManager;

        [SerializeField] private GamePlayManager gamePlayManager;

        [SerializeField] private IPlayerProfileManager playerProfileManager;

        [SerializeField] private ISettingsManager settingsManager;

        public MainUI GetMainUI => mainUI;

        public IDataManager GetDataManager => dataManager;

        public IPlayerProfileManager GetPlayerProfileManager => playerProfileManager;

        public ISettingsManager GetSettingsManager => settingsManager;

        public GamePlayManager GetGamePlayManager => gamePlayManager;

    }
}
