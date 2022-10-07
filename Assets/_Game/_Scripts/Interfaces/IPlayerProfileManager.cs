using LogicPlatformer.Data;
using UnityEngine;

namespace LogicPlatformer.Player
{
    public abstract class IPlayerProfileManager : MonoBehaviour
    {
        private PlayerData playerData;

        public PlayerData GetPlayerData => playerData;

        public virtual void Init(IDataManager dataManager)
        {
            playerData = dataManager.GetPlayerData();
        }
    }
}
