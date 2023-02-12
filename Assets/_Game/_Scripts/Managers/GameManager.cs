using LogicPlatformer.Level;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LogicPlatformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ManagersContainer container;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private int forceLevelNumber = 0;

        [DllImport("__Internal")]
        private static extern void ShowAdv();

        [DllImport("__Internal")]
        private static extern void GetHelpLevelExtern();

        [DllImport("__Internal")]
        private static extern void GetHintExtern();

        private LevelManager levelManager;
        private LevelData levelData;

        public event Action DataChanched;

        private void Awake()
        {
            container.GetDataManager.LoadYandexData();

            GetData();
        }

        private void GetData()
        {
            container.GetDataManager.OnLoadData += () =>
            {
                levelData = container.GetDataManager.DG.levelData;

                container.GetSettingsManager.Init(container.GetDataManager);

                Init();
            };
        }
        private void Init()
        {
            Application.targetFrameRate = 60;

            ShowAdv();

            container.GetMainUI.OnStartGame += () =>
            {
                LoadLevel(levelData.lastOpenLevel);
            };

            container.GetDataManager.SetGameConfig(gameConfig);

            levelData.maxLevels = Resources.LoadAll("Levels/").Length;

            if (forceLevelNumber != 0)
            {
                levelData.lastOpenLevel = forceLevelNumber;
            }

            container.GetMainUI.GetLevelUI.OnRewardedNextLevelClicked += () =>
            {
                StartShowADV();
                GetHelpLevelExtern();
            };
            container.GetMainUI.GetLevelUI.OnTakeHint += () =>
            {
                StartShowADV();
                GetHintExtern();
            };

            container.GetMainUI.Init(levelData, container.GetPlayerProfileManager.GetPlayerData, gameConfig,
                                    container.GetSettingsManager.GetSettingsData);

            container.GetMainUI.OnLiked += () => 
            {
                Debug.Log("Game Liked!");
            };

            container.GetMainUI.OnLevelClicked += LoadLevel;

            container.GetMainUI.GetLevelUI.OnRestartClicked += (int levelIndex) =>
            {
                RestartLevel(levelIndex);
            };

            container.GetGamePlayManager.GetPlayer.IsDead += () =>
            {
                container.GetMainUI.GetLevelUI.Fail();

                //if (container.GetSettingsManager.GetSettingsData.vibrationIsOn)
                //{
                //    Vibration.Vibrate(gameConfig.GetVibrateConfig.strongClicks);
                //}
                StartCoroutine(Wait());
            };

            container.GetMainUI.OnBackLevelRoomClicked += () =>
            {
                container.GetGamePlayManager.GetPlayer.gameObject.SetActive(false);
                Destroy(levelManager.gameObject);
            };

            container.GetMainUI.OnSettingsDataChanged += () =>
            {
                OnSettingsDataChanged();
                container.GetDataManager.SaveSettingsData(container.GetSettingsManager.GetSettingsData);
            };

            if (container.GetSettingsManager.GetSettingsData.musicIsOn &&
                 !container.GetAudioManager.GetBackMusic().isPlaying)
            {
                container.GetAudioManager.GetBackMusic().loop = true;
                container.GetAudioManager.GetBackMusic().Play();
            }
            else
            {
                container.GetAudioManager.GetBackMusic().loop = true;
            }

            DataChanched += container.GetDataManager.SaveData;

            InitSound();

        }

        public void GetHit() //my.jslib
        {
            levelData.levelsHintData[levelData.currentlevel - 1]++;
            container.GetDataManager.SaveLevel(levelData);

            HideADV();
        }

        public void GetLevelHelp() //my.jslib
        {
            LoadNextLevel();
            HideADV();
        }
        private void StartShowADV()
        {
            Time.timeScale = 0f;
            if (container.GetSettingsManager.GetSettingsData.musicIsOn &&
                container.GetAudioManager.GetBackMusic().isPlaying)
            {
                container.GetAudioManager.GetBackMusic().Stop();
            }
        }

        public void HideADV()
        {
            Time.timeScale = 1f;

            if (container.GetSettingsManager.GetSettingsData.musicIsOn &&
                !container.GetAudioManager.GetBackMusic().isPlaying)
            {
                container.GetAudioManager.GetBackMusic().Play();
            }
        }
        private void RestartLevel(int levelIndex)
        {
            container.GetGamePlayManager.GetPlayer.gameObject.SetActive(false);

            LoadLevel(levelIndex);
            container.GetMainUI.OpenLevelUI(levelData, container.GetGamePlayManager.GetPlayer.GetPlayerController);
            Debug.Log("Restart Level");
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
            RestartLevel(levelData.currentlevel);
        }

        private void LoadLevel(int levelIndex)
        {
            if (levelIndex % 2 == 0)
            {
                StartShowADV();
                ShowAdv(); //ADV
            }

            Debug.Log("LoadLevel: " + levelIndex);
            if (levelManager)
            {
                Destroy(levelManager.gameObject);
                //Resources.UnloadUnusedAssets();
            }
            levelData.currentlevel = levelIndex;
            container.GetDataManager.SaveLevel(levelData);

            levelManager = Instantiate(Resources.Load("Levels/Level_" + levelData.currentlevel.ToString("D2"))
                as GameObject).GetComponent<LevelManager>();

            container.GetGamePlayManager.Init(container.GetPlayerProfileManager.GetPlayerData, levelManager);

            container.GetMainUI.OpenLevelUI(levelData, container.GetGamePlayManager.GetPlayer.GetPlayerController);

            container.GetMainUI.SetHints(levelManager);

            levelManager.OnShowSelect += () =>
            {
                container.GetMainUI.ShowSelectButton(true);
            };

            levelManager.OnHideSelect += () =>
            {
                container.GetMainUI.ShowSelectButton(false);
            };

            container.GetMainUI.GetLevelUI.OnClickSelectButton += () =>
            {
                container.GetMainUI.ShowSelectButton(false);
                levelManager.SelectClicked();
            };

            levelManager.OnExitLevel += LoadNextLevel;

        }

        private void LoadNextLevel()
        {
            levelData.currentlevel++;

            if (levelData.currentlevel > levelData.maxLevels)
            {
                levelData.currentlevel = 1;
                levelData.lastOpenLevel = levelData.currentlevel;
                levelData.isOpenAllLevel = true;
            }
            if (levelData.currentlevel > levelData.lastOpenLevel)
            {
                levelData.lastOpenLevel = levelData.currentlevel;
            }
            if (levelData.levelsHintData.Count < levelData.currentlevel)
            {
                Debug.Log("levelData.levelsHintData.Count: " + levelData.levelsHintData.Count);
                levelData.levelsHintData.Add(0);
                DataChanched?.Invoke();
            }

            container.GetDataManager.SaveLevel(levelData);

            LoadLevel(levelData.currentlevel);
        }

        private void OnSettingsDataChanged()
        {
            if (container.GetSettingsManager.GetSettingsData.musicIsOn)
            {
                if (!container.GetAudioManager.GetBackMusic().isPlaying)
                {
                    container.GetAudioManager.GetBackMusic().Play();
                }
            }
            else if (container.GetAudioManager.GetBackMusic().isPlaying)
            {
                container.GetAudioManager.GetBackMusic().Stop();
            }
        }

        private void InitSound()
        {
            container.GetMainUI.OnButtonClicked += () =>
            {
                if (container.GetSettingsManager.GetSettingsData.soundIsOn)
                {
                    container.GetAudioManager.GetUIButton().Play();
                }
            };
        }
    }
}

