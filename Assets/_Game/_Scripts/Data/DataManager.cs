using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer.Data
{
    public class DataManager : MonoBehaviour
    {
        private const string PLAYER_DATA_KEY = "PlayerData";
        private const string SETTINGS_DATA_KEY = "SettingsData";
        private const string LEVEL_DATA_KEY = "LevelData";

        public GameConfig GetGameConfig { get; private set; }
        public void SetGameConfig(GameConfig gameConfig)
        {
            GetGameConfig = gameConfig;
        }
        public PlayerData GetPlayerData()
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
        public void SavePlayerData(PlayerData playerData)
        {
            PlayerPrefs.SetString(PLAYER_DATA_KEY, JsonUtility.ToJson(playerData));
        }

        public SettingsData GetSettingsData()
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
        public void SaveSettingsData(SettingsData settingsData)
        {
            PlayerPrefs.SetString(SETTINGS_DATA_KEY, JsonUtility.ToJson(settingsData));
        }
        public LevelData GetLevelData()
        {
            if (PlayerPrefs.HasKey(LEVEL_DATA_KEY))
            {
                return JsonUtility.FromJson<LevelData>(PlayerPrefs.GetString(LEVEL_DATA_KEY));
            }
            else
            {
                return new LevelData
                {
                    levelsHintData = new List<int>() { 0 },
                    lastOpenLevel = 1,
                    isOpenAllLevel = false
                };
            }
        }

        public void SaveLevel(LevelData levelData)
        {
            PlayerPrefs.SetString(LEVEL_DATA_KEY, JsonUtility.ToJson(levelData));
            PlayerPrefs.Save();
        }


    }
}
