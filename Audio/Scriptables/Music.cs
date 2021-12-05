using UnityEngine;


namespace SmallClasses.Audio.Scriptable {

    [CreateAssetMenu(fileName = "Music", menuName = "Audio/Music", order = 2)]
    public class Music:ScriptableObject {
        public string title; // "Currently playing : <title> by <credits> (<details>)" in UI
        public string credits; 
        public string details; 
        public AudioClip audioClip;
    }
}