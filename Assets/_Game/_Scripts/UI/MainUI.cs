using System;
using System.Collections;
using System.Collections.Generic;
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
        public override event Action OnEndLevel;

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
                levelRoomUI.Open(gameConfig);
            };

            levelRoomUI.OnBackClick += () =>
            {
                levelRoomUI.Close();
                startUI.Open();
            };

            levelUI.OnClickExitButton += () =>
            {
                OnEndLevel?.Invoke();
            };
        }
        public override void Init(LevelData levelData, PlayerData playerData, GameConfig gameConfig, SettingsData settingsData)
        {
            base.Init(levelData, playerData, gameConfig, settingsData);
            startUI.Init();
        }

        public override void OpenLevelUI(LevelData levelData)
        {
            levelUI.Open(levelData);
        }

        public override void ShowExitButton(bool show)
        {
            levelUI.ShowExitButton(show);
        }
    }
}
