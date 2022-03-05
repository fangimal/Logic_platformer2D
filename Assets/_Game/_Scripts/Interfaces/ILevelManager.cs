using UnityEngine;
using LogicPlatformer.Player;
using System;

namespace LogicPlatformer.Level
{
    public abstract class ILevelManager : MonoBehaviour
    {
        [SerializeField] protected IPlayerManager playerManagerPrefabs;

        protected IPlayerManager playerManager;

        protected ILevelGame levelGame;

        public abstract event Action OnOpenedDoor;

        public abstract event Action OnClosedDoor;

        public abstract void StartLevel();

        public abstract void EndLevel();
    }
}
