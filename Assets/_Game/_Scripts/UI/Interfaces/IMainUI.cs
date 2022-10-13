using System;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public abstract class IMainUI : MonoBehaviour
    {
        public abstract event Action OnStartGame;

        public abstract event Action OnOpenedStart;

        public abstract event Action OnLevelRoomOpened;

        public abstract event Action OnBackStart;

        public abstract event Action OnOpenLevel;

        public abstract event Action OnEndLevel;

        public abstract event Action<int> OnLevelClicked;

        protected LevelData levelData;
        protected PlayerData playerData;
        protected GameConfig gameConfig;
        protected SettingsData settingsData;
        public virtual void Init(LevelData levelData, PlayerData playerData, GameConfig gameConfig, SettingsData settingsData)
        {
            this.levelData = levelData;
            this.playerData = playerData;
            this.gameConfig = gameConfig;
            this.settingsData = settingsData;
        }

        public abstract void OpenLevelUI(int levelIndex);

        public abstract void ShowExitButton(bool show);
    }
}
