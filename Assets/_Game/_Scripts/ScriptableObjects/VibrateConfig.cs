using UnityEngine;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "VibrateConfig", menuName = "Config/VibrateConfig", order = 2)]
    public class VibrateConfig : ScriptableObject
    {
        public int lightClicks;
        public int defaultClicks;
        public int strongClicks;
    }
}
