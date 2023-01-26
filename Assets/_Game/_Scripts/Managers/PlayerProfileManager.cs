using LogicPlatformer.Data;
using LogicPlatformer.Player;
using UnityEngine;

namespace LogicPlatformer
{
    public class PlayerProfileManager : MonoBehaviour
    {
        private PlayerData playerData;

        public PlayerData GetPlayerData => playerData;

        public void Init(DataManager dataManager)
        {
            playerData = dataManager.GetPlayerData();
        }
    }
}
