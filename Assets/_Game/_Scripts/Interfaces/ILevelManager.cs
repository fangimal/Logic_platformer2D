using UnityEngine;
using LogicPlatformer.Player;
using System;

namespace LogicPlatformer.Level
{
    public abstract class ILevelManager : MonoBehaviour
    {
        [SerializeField] protected PlayerManager playerManagerPrefabs;

        protected PlayerManager playerManager;

        protected ILevelGame levelGame;



        public abstract void StartLevel();

        public abstract void EndLevel();
    }
}
