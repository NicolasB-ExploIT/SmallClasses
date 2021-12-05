using UnityEngine;

namespace SmallClasses.Audio {

    [DisallowMultipleComponent]
    public class SoundPlayerSingleton : MonoBehaviour
    {
        //static instance of the Player to check if we should keep or delete new instances
        private static SoundPlayerSingleton playerInstance;
        private static float globalVolume = 1.0f;
        private static AudioSource soundSource;
        
        private void Start()
        {
            if (SoundPlayerSingleton.playerInstance == null)
            {
                //dont destroy this instance
                DontDestroyOnLoad(this);
                soundSource = gameObject.AddComponent<AudioSource>();
                //SoundPlayer is intended for small fx sounds
                soundSource.loop = false;
                soundSource.volume = rangeVolume(globalVolume);
                //save a reference to this instance in the static variable
                SoundPlayerSingleton.playerInstance = this;
                #if DEBUG
                Debug.Log("SoundPlayerSingleton loaded");
                #endif
            }
            else
            {
                //Only one instance
                Destroy(this.gameObject);
            }
        }

        private static float rangeVolume(float volume)
        {
            if (volume < 0f) return 0f;
            else if (volume > 1f) return 1f;
            return volume;
        }

        // Use : SoundPlayerSingleton.Play(myClip) or SoundPlayer.Play(myClip, theVolume)
        public static void Play(AudioClip audio, float specificVolume = 1f)
        {
            soundSource.volume = globalVolume * rangeVolume(specificVolume);
            soundSource.clip = audio;
            soundSource.Play();
        }

        // Use : SoundPlayerSingleton.volume = 1.0f;
        public static float volume
        {
            get { return globalVolume; }
            set
            {
                globalVolume = rangeVolume(value);
            }
        }
    }
}