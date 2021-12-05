using UnityEngine;

namespace SmallClasses.Audio.Scriptable {
    
    [CreateAssetMenu(fileName = "Sound", menuName = "Audio/Sound", order = 3)]
    public class Sound:ScriptableObject {
        public string description; // You might want that for test sound UI.
        public AudioClip audioClip;
        [Range(0f, 1f)] public float volume = 1f; 
        //There is a global sound volume in the player, but you might want to have a specific volume per item
    }
}