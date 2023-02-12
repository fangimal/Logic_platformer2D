using LogicPlatformer.Level;
using System;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private StartUI startUI;
        [SerializeField] private LevelUI levelUI;
        [SerializeField] private LevelRoomUI levelRoomUI;
        [SerializeField] private SettingUI settingUI;

        private LevelData levelData;
        public LevelUI GetLevelUI => levelUI;

        public event Action OnStartGame;

        public event Action OnSettingsDataChanged;
        public event Action<int> OnLevelClicked;
        public event Action OnBackLevelRoomClicked;
        public event Action OnButtonClicked;
        public event Action OnLiked;

        private void Awake()
        {
            startUI.Close();
            levelUI.Close();
            levelRoomUI.Close();

            startUI.OnStartGame += () =>
            {
                startUI.Close();
                OnStartGame?.Invoke();
            };

            startUI.OnLevelRoom += () =>
            {
                startUI.Close();
                levelRoomUI.Open(levelData);
            };

            startUI.OnSettings += () =>
            {
                settingUI.Open(() =>
                {
                    OnSettingsDataChanged?.Invoke();
                });
            };

            startUI.OnLikeCliked += () =>
            {
                OnLiked?.Invoke();
            };

            levelRoomUI.OnBackClick += () =>
            {
                levelRoomUI.Close();
                startUI.Open();
            };

            levelRoomUI.OnLevelClicked += (int index) =>
            {
                Debug.Log("OnLevelClicked: " + index);
                OnLevelClicked?.Invoke(index);
                levelRoomUI.Close();
            };

            levelRoomUI.OnButtonClicked += () => 
            {
                OnButtonClicked?.Invoke();
            };

            levelUI.OnBackLevelRoomClicked += () =>
            {
                OnBackLevelRoomClicked?.Invoke();
                levelRoomUI.Open(levelData);
            };

            levelUI.OnSettingsClicked += () => 
            {
                settingUI.Open(() =>
                {
                    OnSettingsDataChanged?.Invoke();
                });
            };

            levelUI.OnButtonClicked += () => 
            { 
                OnButtonClicked?.Invoke(); 
            };

            settingUI.OnButtonClicked += () => 
            {
                OnButtonClicked?.Invoke();
            };
        }
        public void Init(LevelData levelData, PlayerData playerData, GameConfig gameConfig, SettingsData settingsData)
        {
            this.levelData = levelData;
            //this.playerData = playerData;
            //this.gameConfig = gameConfig;
            //this.settingsData = settingsData;

            startUI.Init();
            levelUI.Init(levelData);
            settingUI.Init(settingsData);
        }
        public void SetHints(LevelManager levelManager)
        {
            levelUI.SetHints(levelManager);
        }
        public void OpenLevelUI(LevelData levelData, PlayerController playerController)
        {
            levelUI.Close();
            levelUI.Open(playerController);
        }

        public void ShowSelectButton(bool show)
        {
            levelUI.ShowExitButton(show);
        }
    }
}
