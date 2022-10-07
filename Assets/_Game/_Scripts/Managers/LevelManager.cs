using System;
using UnityEngine;

namespace LogicPlatformer.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform startPlayerPosition;
        public Transform GetStartPlayerPosition => startPlayerPosition;

        public event Action OnOpenedDoor;
        public event Action OnClosedDoor;

        
        public void EndLevel()
        {
            //Destroy(levelGame.gameObject);
            //Destroy(playerManager.gameObject);
        }

        public void StartLevel()
        {
            // TO DO 
            //levelGame = Instantiate(Resources.Load<ILevelGame>("Levels/Level_01"));

            //if (levelGame)
            //{
            //    levelGame.OnShowLevelDoor += () => 
            //    {
            //        OnOpenedDoor?.Invoke();
            //    };

            //    levelGame.OnHideLevelDoor += () =>
            //    {
            //        OnClosedDoor?.Invoke();
            //    };
            //}

            //playerManager = Instantiate(playerManagerPrefabs);
        }
    }
}
