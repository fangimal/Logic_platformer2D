using UnityEngine;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Config/Audio")]
    public class AudioConfig : ScriptableObject
    {
        [Header("BackMusic")]
        [SerializeField] private AudioClip backMusic;

        [Header("Sounds")]
        [SerializeField] private AudioClip uiButton;
        [SerializeField] private AudioClip playerDead;
        [SerializeField] private AudioClip playerMoved;

        public AudioClip GetBackMusic => backMusic;
        public AudioClip GetUIButton => uiButton;
        public AudioClip GetPlayerDead => playerDead;

        public AudioClip GetPlayerMoved => playerMoved;
    }
}
