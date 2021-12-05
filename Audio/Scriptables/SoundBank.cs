using UnityEngine;
namespace SmallClasses.Audio.Scriptable {

    [CreateAssetMenu(fileName = "SoundBank", menuName = "Audio/SoundBank", order = 4)]

    public class SoundBank:GenericList<Sound> { }
    //Generic classes like GenericList<T> can't be made Scriptable or Serializable, but this can
}