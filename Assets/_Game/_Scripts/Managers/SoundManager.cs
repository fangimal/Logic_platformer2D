using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public static class SoundManager
    {
        //private SoundManager instanse;

        //[Serializable]
        //public class Sound
        //{
        //    public string name;
        //    public AudioClip clip;
        //    [Range(0f, 1f)] public float volume = 1f;
        //    [Range(0.1f, 3f)] public float pitch = 1f;
        //    public bool loop = false;

        //    [HideInInspector] public AudioSource source;
        //}

        public enum Sound
        {
            BackSound,
            ButtonClick,
            RedButtonDown,
            PlayerMove,
            PlayerDead,
            OpenGeat,
            KnockKnock,
            Jump,
            Handle,
            Teleportation

        }

        private static Dictionary<Sound, float> soundTimerDictionary;
        private static GameObject oneShotGameObject;
        private static AudioSource oneShotAudioSource;
        private static GameObject backSoundGameObject;
        private static AudioSource backSoundAudioSource;

        public static void Initialize()
        {
            soundTimerDictionary = new Dictionary<Sound, float>();
            soundTimerDictionary[Sound.PlayerMove] = 0;
        }
        public static void PlaySound(Sound sound, Vector2 position)
        {
            if (CanPlaySound(sound) && AudioManager.i.GetSettingsData.soundIsOn)
            {
                GameObject soundGameObject = new GameObject("Sound");
                soundGameObject.transform.position = position;
                AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                audioSource.maxDistance = 100f;
                audioSource.spatialBlend = 1f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.dopplerLevel = 0f;
                audioSource.Play();

                Object.Destroy(soundGameObject, audioSource.clip.length);
            }
        }

        public static void PlaySound(Sound sound)
        {
            if (CanPlaySound(sound) && AudioManager.i.GetSettingsData.soundIsOn)
            {
                if (oneShotGameObject == null)
                {
                    oneShotGameObject = new GameObject("One Shot Sound");
                    oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();

                }
                oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
            }
        }

        public static void PlayBackSound(Sound sound)
        {
            if (AudioManager.i.GetSettingsData.musicIsOn)
            {
                if (backSoundGameObject == null)
                {
                    backSoundGameObject = new GameObject("Back Sound");
                    backSoundAudioSource = backSoundGameObject.AddComponent<AudioSource>();
                    backSoundAudioSource.clip = GetAudioClip(sound);
                    backSoundAudioSource.volume = 0.5f;
                    backSoundAudioSource.loop = true;
                }

                backSoundAudioSource.Play();
            }
            else if (backSoundAudioSource != null)
            {
                backSoundAudioSource.Stop();
            }
        }

        private static bool CanPlaySound(Sound sound)
        {
            switch (sound)
            {
                default:
                    return true;

                case Sound.PlayerMove:
                    if (soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = soundTimerDictionary[sound];
                        float playerMoveTimeMax = 0.5f;
                        if (lastTimePlayed + playerMoveTimeMax < Time.time)
                        {
                            soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                    //break;
            }
        }

        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (AudioManager.SoundAudioClip soundAudioClip in AudioManager.i.SoundAudioClipArray)
            {
                if (soundAudioClip.sound == sound)
                {
                    return soundAudioClip.audioClip;
                }
            }
            Debug.LogError("Sound " + sound + " not found!");
            return null;
        }
    }
}
