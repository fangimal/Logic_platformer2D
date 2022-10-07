using LogicPlatformer.Level;
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
                LoadLevel(levelData);
            };
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
        }
    }
}

