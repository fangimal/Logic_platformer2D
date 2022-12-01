using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public class MainUI : IMainUI
    {
        [SerializeField] private StartUI startUI;
        [SerializeField] private LevelUI levelUI;
        [SerializeField] private LevelRoomUI levelRoomUI;

        public override event Action OnStartGame;
        public override event Action OnOpenedStart;
        public override event Action OnLevelRoomOpened;
        public override event Action OnBackStart;
        public override event Action OnOpenLevel;
        public override event Action OnSelectClicked;
        public override event Action<int> OnLevelClicked;

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

            levelUI.OnClickSelectButton += () =>
            {
                OnSelectClicked?.Invoke();
            };
        }
        public override void Init(LevelData levelData, PlayerData playerData, GameConfig gameConfig, SettingsData settingsData)
        {
            base.Init(levelData, playerData, gameConfig, settingsData);
            startUI.Init();
        }

        public override void OpenLevelUI(int levelIndex)
        {
            levelUI.Open(levelIndex);
        }

        public override void ShowSelectButton(bool show)
        {
            levelUI.ShowExitButton(show);
        }
    }
}
