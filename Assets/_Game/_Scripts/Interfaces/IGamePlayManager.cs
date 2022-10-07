using LogicPlatformer.Data;
using LogicPlatformer.Level;
using LogicPlatformer.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer.GamePlay
{
    public abstract class IGamePlayManager : MonoBehaviour
    {
        public abstract event Action<LevelManager> OnLevelPassed;

        [SerializeField] protected PlayerManager playerManager;

        protected PlayerData playerData;

        protected LevelManager levelManager;
        public PlayerManager GetPlayer => playerManager;

        public virtual void StartGame(PlayerData playerData, LevelManager levelManger)
        {
            this.playerData = playerData;
            this.levelManager = levelManager;
        }
    }
}
