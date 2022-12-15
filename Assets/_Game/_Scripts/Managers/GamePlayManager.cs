using LogicPlatformer.GamePlay;
using LogicPlatformer.Level;
using LogicPlatformer.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;

        public event Action<LevelManager> OnLevelPassed;

        [SerializeField] private PlayerManager player;

        public PlayerManager GetPlayer => player;
        public void Init(PlayerData playerData, LevelManager levelManger)
        {
            //if (player != null)
            //{
            //    Destroy(player.gameObject);
            //    Debug.Log("Destroy Player");
            //}

            //player = Instantiate(playerManager);
            player.Initialize(playerData, levelManger.GetStartPlayerPosition);
        }
    }
}
