using LogicPlatformer.Level;
using System.Collections;
using UnityEngine;

namespace LogicPlatformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ManagersContainer container;
        [SerializeField] private GameConfig gameConfig;

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

            levelData = container.GetDataManager.GetLevelData(gameConfig);

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
        }
        private void RestartLevel(int levelIndex)
        {
            container.GetGamePlayManager.GetPlayer.gameObject.SetActive(false);
            UnSubsribeLevel();
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
        private void UnSubsribeLevel()
        {
            levelManager.OnExitLevel -= LoadNextLevel;

            container.GetMainUI.OnLevelClicked -= LoadLevel;
        }
        private void LoadNextLevel()
        {
            UnSubsribeLevel();

            levelData.currentlevel++;

            if (levelData.currentlevel > levelData.maxLevels)
            {
                levelData.currentlevel = 1;
                levelData.lastOpenLevel = levelData.currentlevel;
            }
            if (levelData.currentlevel > levelData.lastOpenLevel)
            {
                levelData.lastOpenLevel = levelData.currentlevel;
            }

            container.GetDataManager.SaveLevel(levelData);

            LoadLevel(levelData.currentlevel);
            //Debug.Log("currentlevel: " + levelData.currentlevel + ", lastOpenLevel: " + levelData.lastOpenLevel + ", max:" +levelData.maxLevels);
        }
    }
}

