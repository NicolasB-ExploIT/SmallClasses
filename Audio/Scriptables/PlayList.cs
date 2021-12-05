using UnityEngine;


namespace SmallClasses.Audio.Scriptable {

    [CreateAssetMenu(fileName = "PlayList", menuName = "Audio/PlayList", order = 1)]
    public class PlayList:GenericList<Music> { }
    //Generic classes like GenericList<T> can't be made Scriptable or Serializable, but this can
}