using UnityEngine;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "VibrateConfig", menuName = "Config/VibrateConfig", order = 3)]
    public class VibrateConfig : ScriptableObject
    {
        public int lightClicks;
        public int defaultClicks;
        public int strongClicks;
    }
}
