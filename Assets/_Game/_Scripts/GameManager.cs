using UnityEngine;

namespace LogicPlatformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ManagersContainer container;

        void Start()
        {
            container.GetMainUI.GetStartActions.OnStarted += () =>
            {
                OnStartButton();
            };

            OpenStartScreen();


            //container.GetLevelController.OnShowLevelDoor += () =>
            //{
            //    container.GetMainUI.GetLevelMainUI.ShowExitButton(true);
            //};

            //container.GetLevelController.OnHideLevelDoor += () =>
            //{
            //    container.GetMainUI.GetLevelMainUI.ShowExitButton(false);
            //};
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

            Resources.UnloadUnusedAssets();

            container.GetLevelManager.EndLevel();

            container.GetMainUI.GetLevelUIActions.OnEndLevel -= CloseLevel;

            OpenStartScreen();
        }
    }
}

