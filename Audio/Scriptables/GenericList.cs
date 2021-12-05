using UnityEngine;

namespace SmallClasses.Audio.Scriptable {
    public class GenericList<T>:ScriptableObject {
        public T[] list; // The list is not planned to change during playtime so Array > List<> here
        private int currentIndex = 0;

        public T GetAt(int index) {
        #if DEBUG
        if( list.Length < 1 ) Debug.LogWarning($"You forgot to assign '{nameof(T)}' items to your list");
        #endif
        return (list[index]);
        }

        public T Current(){
            return GetAt(currentIndex);
        }
        public T First(){
            currentIndex = 0;
            return Current();
        }

        public T Last(){
            currentIndex = list.Length - 1 ;
            return Current();
        }

        public T Next(){
            if(++currentIndex < list.Length ) return Current();
            return First();
        }

        public T Previous(){
            if(--currentIndex > -1 ) return Current();
            return Last();
        }
    }
}