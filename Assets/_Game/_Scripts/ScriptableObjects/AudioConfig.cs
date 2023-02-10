using UnityEngine;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Config/Audio")]
    public class AudioConfig : ScriptableObject
    {
        [Header("BackMusic")]
        [SerializeField] private AudioClip backMusic;
        public AudioClip GetBackMusic => backMusic;

        [Header("Sounds")]
        [SerializeField] private AudioClip uiButton;
        [SerializeField] private AudioClip playerDead;

        public AudioClip GetUIButton => uiButton;
        public AudioClip GetPlayerDead => playerDead;
    }
}
