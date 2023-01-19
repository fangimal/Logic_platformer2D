using LogicPlatformer.Level;
using System.Collections;
using UnityEngine;

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

            container.GetDataManager.SetGameConfig(gameConfig);

            levelData = container.GetDataManager.GetLevelData();

            container.GetSettingsManager.Init(container.GetDataManager);

            if (forceLevelNumber != 0)
            {
                levelData.lastOpenLevel = forceLevelNumber;
            }

            container.GetMainUI.GetLevelUI.OnNeedHelpClicked += () =>
            {
                //todo reward
                levelData.levelsHintData[levelData.currentlevel - 1]++;
                container.GetDataManager.SaveLevel(levelData);
            };

            container.GetMainUI.GetLevelUI.OnRewardedNextLevelClicked += () =>
            {
                //todo reward
                LoadNextLevel();
            };
            container.GetMainUI.GetLevelUI.OnTakeHint += () =>
            {
                levelData.levelsHintData[levelData.levelsHintData.Count - 1]++;
                container.GetDataManager.SaveLevel(levelData);
            };

            levelData.maxLevels = Resources.LoadAll("Levels/").Length;

            container.GetMainUI.Init(levelData, container.GetPlayerProfileManager.GetPlayerData, gameConfig,
                                    container.GetSettingsManager.GetSettingsData);

            container.GetMainUI.OnLevelClicked += LoadLevel;

            container.GetMainUI.GetLevelUI.OnRestartClicked += (int levelIndex) =>
            {
                RestartLevel(levelIndex);
            };

            container.GetGamePlayManager.GetPlayer.IsDead += () =>
            {
                container.GetMainUI.GetLevelUI.Fail();
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

            InitSound();
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

