using UnityEngine;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private VibrateConfig vibrate;

        public VibrateConfig GetVibrateConfig => vibrate;
    }
}
