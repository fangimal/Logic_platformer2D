using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.Data
{
    public class DataGroup
    {
        public LevelData levelData ;
        public SettingsData settingsData;

        public DataGroup() 
        {
            levelData = new LevelData();
            settingsData = new SettingsData();
        }
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

        //VK

        [DllImport("__Internal")]
        private static extern void Auth();

        [DllImport("__Internal")]
        private static extern void GetData();

        [DllImport("__Internal")]
        private static extern void SetData(string data);


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

            //CurrentLanguage = GetLang();
        }
        public void SetGameConfig(GameConfig gameConfig)
        {
            GetGameConfig = gameConfig;
        }

        public void LoadYandexData()
        {
            //LoadExtern();
            GettingData();
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
            Debug.Log("Save! DataGetting: " + DG + ". String data: " + jsonString);
#if UNITY_WEBGL
            //SaveExtern(jsonString);
            SetData(jsonString);
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
        }
        public void SaveSettingsData(SettingsData settingsData)
        {
            DG.settingsData = settingsData;
            SaveData();
        }
        public LevelData GetLevelData()
        {
            return DG.levelData;

        }

        public void SaveLevel(LevelData levelData)
        {
            DG.levelData = levelData;
            SaveData();
        }

        //VK

        public void GettingData()    // Получение данных
        {
            GetData();
        }
        public void DataGetting(string data)
        {
            string parsText = data.Replace("\\", "");
            string st1 = parsText.Substring(parsText.IndexOf("levelData") - 2);
            string st2 = st1.Remove(st1.Length - 4);

            DG = JsonUtility.FromJson<DataGroup>(st2);

            if (DG.settingsData.saveLang == false)
            {
                DG = new DataGroup();
                SetDefaultLang();
                SaveData();
            }

            OnLoadData?.Invoke();

            Debug.Log("Load data scrips! DataGetting: " + DG + ". st2: " + st2);
        }

        public void FirstGetData()
        {
            Debug.Log("SET NEW DATA! FirstGetData");
            DG = new DataGroup();
            SetDefaultLang();
            SaveData();
            OnLoadData?.Invoke();
        }

    }
}
