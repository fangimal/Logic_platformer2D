using LogicPlatformer.Level;
using LogicPlatformer.Player;
using UnityEngine;

namespace LogicPlatformer
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;

        [SerializeField] private PlayerManager player;

        public PlayerManager GetPlayer => player;
        public void Init(PlayerData playerData, LevelManager levelManger)
        {
            player.Initialize(playerData, levelManger);
        }
    }
}
