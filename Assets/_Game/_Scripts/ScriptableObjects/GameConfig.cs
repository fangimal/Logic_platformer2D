using UnityEngine;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int forceLevelNumber;
        [SerializeField] private int maxLevel = 3;

        public int GetForceLevelNumber => forceLevelNumber;

        public int GetMaxLevel => maxLevel;

    }
}
