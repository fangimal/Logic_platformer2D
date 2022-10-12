using LogicPlatformer.Level;
using UnityEditor.PackageManager;
using UnityEngine;

namespace LogicPlatformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ManagersContainer container;
        [SerializeField] private GameConfig gameConfig;

        private LevelManager levelManager;
        private LevelData levelData;
        private int levelCount;

        private void Awake()
        {
            Init();

            container.GetMainUI.OnStartGame += () =>
            {
                LoadLevel(levelData);
            };

            levelCount = Resources.LoadAll("Levels/").Length;
            //Debug.Log("levelCount: " + levelCount.Length);
        }

        private void Init()
        {
            Application.targetFrameRate = 60;

            container.GetDataManager.SetGameConfig(gameConfig);

            levelData = container.GetDataManager.GetLevelData(gameConfig);

            container.GetMainUI.Init(levelData, container.GetPlayerProfileManager.GetPlayerData, gameConfig, container.GetSettingsManager.GetSettingsData);
        }

        private void LoadLevel(LevelData levelData)
        {
            if (levelManager)
            {
                Destroy(levelManager.gameObject);
                Resources.UnloadUnusedAssets();
            }
            Debug.Log("Load Level: " + levelData.number);

            levelManager = Instantiate(Resources.Load("Levels/Level_" + levelData.number.ToString("D2"))
                as GameObject).GetComponent<LevelManager>();

            container.GetGamePlayManager.GetPlayer.Initialize(levelManager.GetStartPlayerPosition);

            container.GetMainUI.OpenLevelUI(levelData);

            levelManager.OnOpenedDoor += () =>
            {
                container.GetMainUI.ShowExitButton(true);
            };

            levelManager.OnClosedDoor += () =>
            {
                container.GetMainUI.ShowExitButton(false);
            };

            container.GetMainUI.OnEndLevel += () =>
            {
                LoadNextLevel(levelData);
            };

            levelManager.OnExitLevel += () =>
            {
                LoadNextLevel(levelData);
            };
        }

        private void LoadNextLevel(LevelData levelData)
        {
            levelData.number++;

            if (levelData.number > levelCount)
            {
                levelData.number = 1;
            }

            container.GetDataManager.SaveLevel(levelData);
            LoadLevel(levelData);
        }
    }
}

