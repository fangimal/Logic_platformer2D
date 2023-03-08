using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.Data
{
    public class DataGroup
    {
        public LevelData levelData = new LevelData();
        public SettingsData settingsData = new SettingsData();
    }
    public class DataManager : MonoBehaviour
    {
        private const string PLAYER_DATA_KEY = "PlayerData";
        private const string SETTINGS_DATA_KEY = "SettingsData";
        private const string LEVEL_DATA_KEY = "LevelData";

        [SerializeField] private Button clearButton;

        [DllImport("__Internal")]
        private static extern void SaveExtern(string date);

        [DllImport("__Internal")]
        private static extern void LoadExtern();

        //[DllImport("__Internal")]
        //private static extern void GiveMePlayerData(); //auth

        [DllImport("__Internal")]
        private static extern string GetLang();

        [DllImport("__Internal")]
        private static extern void RateGame();

        private string CurrentLanguage;

        public DataGroup DG = new DataGroup();

        public event Action OnLoadData;

        public GameConfig GetGameConfig { get; private set; }

        private void Awake()
        {
            clearButton.onClick.AddListener(() =>
            {
                DG = new DataGroup();
                SaveData();
            });

            CurrentLanguage = GetLang();
        }
        public void SetGameConfig(GameConfig gameConfig)
        {
            GetGameConfig = gameConfig;
        }

        public void LoadYandexData()
        {
            LoadExtern();
        }

        public void LoadData(string data)
        {
            DG = JsonUtility.FromJson<DataGroup>(data);

            if (DG.settingsData.saveLang == false)
            {
                SetDefaultLang();
            }

            OnLoadData?.Invoke();
        }

        private void SetDefaultLang()
        {
            switch (CurrentLanguage)
            {
                case "en":
                    DG.settingsData.langIndex = 0;
                    break;
                case "ru":
                    DG.settingsData.langIndex = 1;
                    break;
                case "tr":
                    DG.settingsData.langIndex = 2;
                    break;
                default:
                    DG.settingsData.langIndex = 1;
                    break;
            }

            DG.settingsData.saveLang = true;
        }

        public void SaveData()
        {
            string jsonString = JsonUtility.ToJson(DG);
#if UNITY_WEBGL
            SaveExtern(jsonString);
#endif
        }

        public void RateGameButton() //button, my.jslib
        {
#if UNITY_WEBGL
            RateGame();
#endif
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
            return DG.settingsData;

            //bool hasData = PlayerPrefs.HasKey(SETTINGS_DATA_KEY);
            //if (hasData)
            //{
            //    return JsonUtility.FromJson<SettingsData>(PlayerPrefs.GetString(SETTINGS_DATA_KEY));
            //}
            //else
            //{
            //    return new SettingsData
            //    {
            //        vibrationIsOn = true,
            //        soundIsOn = true,
            //        musicIsOn = true
            //    };
            //}

        }
        public void SaveSettingsData(SettingsData settingsData)
        {
            DG.settingsData = settingsData;
            SaveData();
            //PlayerPrefs.SetString(SETTINGS_DATA_KEY, JsonUtility.ToJson(settingsData));
        }
        public LevelData GetLevelData()
        {
            return DG.levelData;

            //if (PlayerPrefs.HasKey(LEVEL_DATA_KEY))
            //{
            //    return JsonUtility.FromJson<LevelData>(PlayerPrefs.GetString(LEVEL_DATA_KEY));
            //}
            //else
            //{
            //    return new LevelData
            //    {
            //        levelsHintData = new List<int>() { 0 },
            //        lastOpenLevel = 1,
            //        isOpenAllLevel = false
            //    };
            //}
        }

        public void SaveLevel(LevelData levelData)
        {
            DG.levelData = levelData;
            SaveData();
            //PlayerPrefs.SetString(LEVEL_DATA_KEY, JsonUtility.ToJson(levelData));
            //PlayerPrefs.Save();
        }


    }
}
