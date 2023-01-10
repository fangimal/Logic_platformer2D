using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer.Data
{
    public abstract class IDataManager : MonoBehaviour
    {
        public GameConfig GetGameConfig { get; private set; }

        public void SetGameConfig(GameConfig gameConfig)
        {
            GetGameConfig = gameConfig;
        }

        public abstract PlayerData GetPlayerData();

        public abstract void SavePlayerData(PlayerData playerData);

        public abstract SettingsData GetSettingsData();

        public abstract void SaveSettingsData(SettingsData settingsData);

        public abstract void SaveLevel(LevelData levelData);

        public abstract LevelData GetLevelData();
    }
}
