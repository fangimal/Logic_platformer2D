using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioConfig audioConfig;

        private static Dictionary<string, AudioSource> audios;

        private static GameObject audioContainer;

        private AudioSource GetAudio(AudioClip audioClip)
        {
            InitAudions();

            if (audioClip)
            {
                if (!audios.ContainsKey(audioClip.name))
                {
                    SetupClip(audioClip);
                }

                return audios[audioClip.name];
            }
            else
            {
                return null;
            }
        }

        private void SetupClip(AudioClip clip, bool loop = false)
        {
            if (clip != null)
            {
                AudioSource audioSource = audioContainer.AddComponent<AudioSource>();
                audioSource.loop = loop;
                audioSource.playOnAwake = false;
                audioSource.clip = clip;
                audios.Add(clip.name, audioSource);
            }
        }

        private void InitAudions()
        {
            if (audioContainer == null && audios == null)
            {
                audioContainer = new GameObject("AudioContainer");

                DontDestroyOnLoad(audioContainer);

                audios = new Dictionary<string, AudioSource>();

                SetupClip(audioConfig.GetUIButton);
            }
        }

        public AudioSource GetBackMusic()
        {
            return GetAudio(audioConfig.GetBackMusic);
        }
        public AudioSource GetUIButton()
        {
            return GetAudio(audioConfig.GetUIButton);
        }
    }
}
