using System;
using UnityEngine;

namespace LogicPlatformer.Level
{
    public class LevelManager : ILevelManager //CrazyRunnerManager
    {
        public override event Action OnOpenedDoor;
        public override event Action OnClosedDoor;

        public override void EndLevel()
        {
            Destroy(levelGame.gameObject);
            Destroy(playerManager.gameObject);
        }

        public override void StartLevel()
        {
            // TO DO 
            levelGame = Instantiate(Resources.Load<ILevelGame>("Levels/Level_01"));

            if (levelGame)
            {
                levelGame.OnShowLevelDoor += () => 
                {
                    OnOpenedDoor?.Invoke();
                };

                levelGame.OnHideLevelDoor += () =>
                {
                    OnClosedDoor?.Invoke();
                };
            }

            playerManager = Instantiate(playerManagerPrefabs);
        }
    }
}
