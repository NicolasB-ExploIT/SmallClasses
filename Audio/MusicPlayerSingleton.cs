using UnityEngine;
using System.Collections;

namespace SmallClasses.Audio {
    
    [DisallowMultipleComponent]
    public class MusicPlayerSingleton : MonoBehaviour
    {
        //static instance of the MusicPlayer to check if we should keep or delete new instances
        private static MusicPlayerSingleton playerInstance;
        private static float globalVolume = 1f;
        private const float fadeOutDuration = 1.2f;
        private const float fadeInDuration = 1.2f;
        private static AudioSource musicSource;
        private static AudioClip nextMusicClip;
        

        private void Start()
        {            
            if (MusicPlayerSingleton.playerInstance == null)
            {
                //dont destroy this instance
                DontDestroyOnLoad(this);
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.volume = RangeVolume(globalVolume);
                //save a reference to this instance in the static variable
                MusicPlayerSingleton.playerInstance = this;
                #if DEBUG
                Debug.Log("MusicPlayerSingleton loaded");
                #endif
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private static float RangeVolume(float volume) {
            if(volume < 0f) return 0f;
            else if(volume > 1f) return 1f;
            return volume;
        }

        // Use : MusicPlayerSingleton.Play(myClip)
        public static void Play(AudioClip audioClip) {
            if(audioClip != null) {
                nextMusicClip = audioClip;
                playerInstance.StartCoroutine(playerInstance.FadeOut());
                        
            }
        }

        public IEnumerator FadeOut()
        {
            float currentTime = 0;
            if(musicSource.isPlaying) {
                while (currentTime < fadeOutDuration)
                {
                    currentTime += Time.deltaTime;
                    musicSource.volume = Mathf.Lerp(globalVolume, 0, currentTime / fadeOutDuration);
                    yield return null;
                }
                musicSource.Stop();
            }        
            StartCoroutine(FadeIn());
            yield break;
        }

        public IEnumerator FadeIn()
        {
            float currentTime = 0;
            musicSource.clip = nextMusicClip;
            musicSource.Play();
            while (currentTime < fadeInDuration)
            {
                currentTime += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(0, globalVolume, currentTime / fadeInDuration);
                yield return null;
            }
            yield break;
        }

        // Use : MusicPlayerSingleton.volume = 1.0f;
        public static float volume
        {
            get { return globalVolume; }
            set { 
                globalVolume = RangeVolume(value);
                musicSource.volume = globalVolume;
            }
        }
    }
}