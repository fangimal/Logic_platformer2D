using System;
using UnityEngine;

namespace LogicPlatformer.Level
{
    public class LevelGame : ILevelGame
    {
        [SerializeField] private ExitDoor exitDoor;

        public override event Action OnShowLevelDoor;

        public override event Action OnHideLevelDoor;

        public override event Action OnStartLevel;

        private void Awake()
        {
            exitDoor.OnDoorOpened += () =>
            {
                OnShowLevelDoor?.Invoke();
                Debug.Log("Enter Hero!");
            };

            exitDoor.OnDoorClosed += () =>
            {
                OnHideLevelDoor?.Invoke();
                Debug.Log("NO!");
            };
        }
    }
}
