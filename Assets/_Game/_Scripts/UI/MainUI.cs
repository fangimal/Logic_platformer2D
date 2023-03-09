using LogicPlatformer.Level;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace LogicPlatformer.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private StartUI startUI;
        [SerializeField] private LevelUI levelUI;
        [SerializeField] private LevelRoomUI levelRoomUI;
        [SerializeField] private SettingUI settingUI;

        private LevelData levelData;
        private SettingsData settingsData;
        private bool active = false;
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

            startUI.OnClicked += () =>
            {
                OnButtonClicked?.Invoke();
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

            settingUI.OnLanguageChanged += (index) =>
            {
                settingsData.langIndex = index;
                OnSettingsDataChanged?.Invoke();
                ChangeLocale(settingsData.langIndex);
            };
        }
        public void Init(LevelData levelData, PlayerData playerData, GameConfig gameConfig, SettingsData settingsData)
        {
            this.levelData = levelData;
            this.settingsData = settingsData;
            //this.playerData = playerData;
            //this.gameConfig = gameConfig;

            startUI.Init();
            levelUI.Init(levelData, gameConfig.GetLocalConfig);
            settingUI.Init(settingsData, gameConfig.GetLocalConfig);

            ChangeLocale(settingsData.langIndex);
        }
        public void SetHints()
        {
            levelUI.SetHints();
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

        public void ChangeLocale(int localeID)
        {
            if (active)
            {
                return;
            }
            StartCoroutine(SetLocal(localeID));
        }

        private IEnumerator SetLocal(int _localeID)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
            active = false;
        }
    }
}
