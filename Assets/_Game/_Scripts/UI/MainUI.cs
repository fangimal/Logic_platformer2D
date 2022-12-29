using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private StartUI startUI;
        [SerializeField] private LevelUI levelUI;
        [SerializeField] private LevelRoomUI levelRoomUI;

        private LevelData levelData;
        private PlayerData playerData;
        private GameConfig gameConfig;
        private SettingsData settingsData;

        public LevelUI GetLevelUI => levelUI;

        public event Action OnStartGame;
        public event Action OnOpenedStart;
        public event Action OnLevelRoomOpened;
        public event Action OnBackStart;
        public event Action OnOpenLevel;
        public event Action OnSelectClicked;
        public event Action<int> OnLevelClicked;
        public event Action OnBackLevelRoomClicked;

        private void Awake()
        {
            startUI.Open();
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

            levelRoomUI.OnBackClick += () =>
            {
                levelRoomUI.Close();
                startUI.Open();
            };

            levelRoomUI.OnLevelClicked += (int index) =>
            {
                OnLevelClicked?.Invoke(index);
                levelRoomUI.Close();
            };

            levelUI.OnBackLevelRoomClicked += () =>
            {
                OnBackLevelRoomClicked?.Invoke();
                levelRoomUI.Open(levelData);
            };

        }
        public void Init(LevelData levelData, PlayerData playerData, GameConfig gameConfig, SettingsData settingsData)
        {
            this.levelData = levelData;
            this.playerData = playerData;
            this.gameConfig = gameConfig;
            this.settingsData = settingsData;

            startUI.Init();

        }

        public void OpenLevelUI(LevelData levelData, PlayerController playerController)
        {
            levelUI.Close();
            levelUI.Open(levelData);
            levelUI.Init(playerController);
        }

        public void ShowSelectButton(bool show)
        {
            levelUI.ShowExitButton(show);
        }
    }
}
