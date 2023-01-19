using UnityEngine;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Config/Audio")]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField] private AudioClip backMusic;

        [Header("Sounds")]
        [SerializeField] private AudioClip uiButton;

        public AudioClip GetUIButton => uiButton;
        public AudioClip GetBackMusic => backMusic;
    }
}
