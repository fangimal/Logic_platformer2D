using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer.Data
{
    public class DataManager : IDataManager
    {
        private const string PLAYER_DATA_KEY = "PlayerData";
        private const string SETTINGS_DATA_KEY = "SettingsData";
        private const string LEVEL_DATA_KEY = "LevelData";


        public override PlayerData GetPlayerData()
        {
            bool hasData = PlayerPrefs.HasKey(PLAYER_DATA_KEY);

            if (hasData)
            {
                return JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PLAYER_DATA_KEY));
            }
            else
            {
                return new PlayerData
                {
                    currentSkinNumber = 0,
                    lastUnlockSkin = 0
                };
            }
        }
        public override void SavePlayerData(PlayerData playerData)
        {
            PlayerPrefs.SetString(PLAYER_DATA_KEY, JsonUtility.ToJson(playerData));
        }

        public override SettingsData GetSettingsData()
        {
            bool hasData = PlayerPrefs.HasKey(SETTINGS_DATA_KEY);

            if (hasData)
            {
                return JsonUtility.FromJson<SettingsData>(PlayerPrefs.GetString(SETTINGS_DATA_KEY));
            }
            else
            {
                return new SettingsData
                {
                    vibrationIsOn = true,
                    soundIsOn = true,
                    musicIsOn = true
                };
            }
        }
        public override void SaveSettingsData(SettingsData settingsData)
        {
            PlayerPrefs.SetString(SETTINGS_DATA_KEY, JsonUtility.ToJson(settingsData));
        }
        public override LevelData GetLevelData(GameConfig gameConfig)
        {
            if (gameConfig.GetForceLevelNumber != 0)
            {
                return new LevelData { number = gameConfig.GetForceLevelNumber };
            }
            if (PlayerPrefs.HasKey(LEVEL_DATA_KEY))
            {
                return JsonUtility.FromJson<LevelData>(PlayerPrefs.GetString(LEVEL_DATA_KEY));
            }
            return new LevelData { number = 1 };
        }

        public override void SaveLevel(LevelData levelData)
        {
            PlayerPrefs.SetString(LEVEL_DATA_KEY, JsonUtility.ToJson(levelData));
            PlayerPrefs.Save();
        }


    }
}
