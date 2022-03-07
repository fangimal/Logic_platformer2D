using UnityEngine;

namespace LogicPlatformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ManagersContainer container;

        void Start()
        {
            OpenStartScreen();

            container.GetMainUI.GetStartActions.OnStarted += () =>
            {
                OnStartButton();
            };

            container.GetMainUI.GetStartActions.OnLevelRoomOpened += () =>
            {
                CloseStart();
                OpenLevelRoom();
            };

            container.GetMainUI.GetLevelRoomUIActions.OnBack += () =>
            {
                CloseLevelRoom();
            };
        }

        private void OpenStartScreen()
        {
            //To do GameState 
            Debug.Log("OpenStartScreen");

            container.GetMainUI.InitStartsUI();
        }

        private void OnStartButton()
        {
            CloseStart();

            Resources.UnloadUnusedAssets();

            OpenLevel();

            container.GetLevelManager.OnOpenedDoor += () =>
            {
                container.GetMainUI.GetLevelMainUI.ShowExitButton(true);
            };

            container.GetLevelManager.OnClosedDoor += () =>
            {
                container.GetMainUI.GetLevelMainUI.ShowExitButton(false);
            };

            container.GetLevelManager.StartLevel();

            container.GetMainUI.GetLevelUIActions.OnEndLevel += CloseLevel;

        }

        private void CloseStart()
        {
            Debug.Log("CloseStart");

            container.GetMainUI.Clear();
        }

        private void OpenLevel()
        {
            //To do GameState 
            Debug.Log("Open Level Screen");

            container.GetMainUI.InitLevelsUI();
        }

        private void CloseLevel()
        {
            container.GetMainUI.Clear();

            container.GetLevelManager.EndLevel();

            container.GetMainUI.GetLevelUIActions.OnEndLevel -= CloseLevel;

            OpenStartScreen();
        }

        private void OpenLevelRoom()
        {
            container.GetMainUI.InitLevelRoomUI();
        }

        private void CloseLevelRoom()
        {
            container.GetMainUI.Clear();

            OpenStartScreen();
        }
    }
}

