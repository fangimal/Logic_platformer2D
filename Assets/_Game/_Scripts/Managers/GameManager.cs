using LogicPlatformer.Level;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;

namespace LogicPlatformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ManagersContainer container;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private int forceLevelNumber = 0;

        private LevelManager levelManager;
        private LevelData levelData;

        private void Awake()
        {
            Init();

            container.GetMainUI.OnStartGame += () =>
            {
                LoadLevel(levelData.lastOpenLevel);
            };
        }

        private void Init()
        {
            Application.targetFrameRate = 60;

            //container.GetAdsManager.Initialize();

            container.GetAdsInitializer.InitializeAds();

            container.GetAdsInitializer.GetRewardedAds.OnTakeHint += GetHit;

            container.GetAdsInitializer.GetRewardedAds.OnOpenNextLevel += GetLevelHelp;

            container.GetAdsInitializer.GetInterstitialAds.OnCompleteShowdAds += () => 
            {
                Time.timeScale = 1f;
                HideADV(); 
            };

            container.GetDataManager.SetGameConfig(gameConfig);

            levelData = container.GetDataManager.GetLevelData();

            container.GetSettingsManager.Init(container.GetDataManager);

            AudioManager.i.SetData(container.GetSettingsManager.GetSettingsData);

            if (forceLevelNumber != 0)
            {
                levelData.lastOpenLevel = forceLevelNumber;

                TakeHintsForTest();
            }

            container.GetMainUI.GetLevelUI.OnRewardedNextLevelClicked += () =>
            {
                StartShowADV();
                container.GetAdsInitializer.GetRewardedAds.ShowRewardVideo(false);
                //GetLevelHelp();//my.jslib
            };
            container.GetMainUI.GetLevelUI.OnTakeHint += () =>
            {
                StartShowADV();
                container.GetAdsInitializer.GetRewardedAds.ShowRewardVideo(true);
                //GetHit(); //my.jslib
            };

            levelData.maxLevels = Resources.LoadAll("Levels/").Length;

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

                if (container.GetSettingsManager.GetSettingsData.vibrationIsOn)
                {
                    Vibration.Vibrate(gameConfig.GetVibrateConfig.strongClicks);
                }
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

            InitSound();
        }
        private void TakeHintsForTest()
        {
            int startIndex = levelData.levelsHintData.Count - 1;

            for (int i = startIndex; i < levelData.lastOpenLevel - 1; i++)
            {
                levelData.levelsHintData.Add(0);
            }
        }
        public void GetHit() //my.jslib
        {
            levelData.levelsHintData[levelData.currentlevel - 1]++;

            container.GetDataManager.SaveLevel(levelData);
            container.GetMainUI.GetLevelUI.GetLevelHelper.AfterADV();
            HideADV();
        }

        public void GetLevelHelp() //my.jslib
        {
            Time.timeScale = 1f;
            LoadNextLevel();
            HideADV();
        }
        private void StartShowADV()
        {
            Time.timeScale = 0f;

            SoundManager.GetSoundStatus = container.GetSettingsManager.GetSettingsData.musicIsOn;

            container.GetSettingsManager.GetSettingsData.musicIsOn = false;

            OnSettingsDataChanged();
        }

        private void HideADV()
        {
            //Time.timeScale = 1f;

            container.GetSettingsManager.GetSettingsData.musicIsOn = SoundManager.GetSoundStatus;

            OnSettingsDataChanged();
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
            Debug.Log("LoadLevel: " + levelIndex);

            if (levelIndex % 2 == 0)
            {
                StartShowADV();
                container.GetAdsInitializer.GetInterstitialAds.ShowAd(); //ADV
            }

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

            container.GetMainUI.SetHints();

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
            }

            container.GetDataManager.SaveLevel(levelData);

            LoadLevel(levelData.currentlevel);

            container.GetAnalyticsManager.AnaliticLoadLevel(levelData.currentlevel);
        }

        private void OnSettingsDataChanged()
        {
            SoundManager.PlayBackSound(SoundManager.Sound.BackSound);
        }

        private void InitSound()
        {
            SoundManager.PlayBackSound(SoundManager.Sound.BackSound);

            container.GetMainUI.OnButtonClicked += () =>
            {
                SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            };

            container.GetGamePlayManager.GetPlayer.IsDead += () =>
            {
                SoundManager.PlaySound(SoundManager.Sound.PlayerDead, container.GetGamePlayManager.GetPlayer.transform.position);
            };

            container.GetGamePlayManager.GetPlayer.GetPlayerController.PlayerMoved += () =>
            {
                SoundManager.PlaySound(SoundManager.Sound.PlayerMove, container.GetGamePlayManager.GetPlayer.transform.position);
            };
        }
    }
}

