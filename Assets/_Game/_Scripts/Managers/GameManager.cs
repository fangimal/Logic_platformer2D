using LogicPlatformer.Level;
using System.ComponentModel;
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

            container.GetMainUI.Init(levelData, container.GetPlayerProfileManager.GetPlayerData, gameConfig, container.GetSettingsManager.GetSettingsData);

            container.GetMainUI.OnLevelClicked += LoadLevel;
        }

        private void LoadLevel(int levelIndex)
        {
            if (levelManager)
            {
                Destroy(levelManager.gameObject);
                Resources.UnloadUnusedAssets();
            }
            levelData.currentlevel = levelIndex;
            container.GetDataManager.SaveLevel(levelData);

            levelManager = Instantiate(Resources.Load("Levels/Level_" + levelData.currentlevel.ToString("D2"))
                as GameObject).GetComponent<LevelManager>();

            container.GetGamePlayManager.GetPlayer.Initialize(levelManager.GetStartPlayerPosition);

            container.GetMainUI.OpenLevelUI(levelData.currentlevel);

            levelManager.OnShowSelect += () =>
            {
                container.GetMainUI.ShowSelectButton(true);
            };

            levelManager.OnHideSelect += () =>
            {
                container.GetMainUI.ShowSelectButton(false);
            };

            container.GetMainUI.OnSelectClicked += () =>
            {
                container.GetMainUI.ShowSelectButton(false);
                levelManager.SelectClicked();
            };

            levelManager.OnExitLevel += LoadNextLevel;

        }

        private void LoadNextLevel()
        {
            container.GetMainUI.OnSelectClicked -= LoadNextLevel;

            levelManager.OnExitLevel -= LoadNextLevel;

            container.GetMainUI.OnLevelClicked -= LoadLevel;

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

