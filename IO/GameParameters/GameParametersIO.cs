using System.IO;
using UnityEngine;

namespace SmallClasses.IO {
    public class GameParametersIO
    {
        // Create a field of this class for the file.
        public GameParameters gameParameters;
        private string saveFile;
        
        public GameParametersIO(string filename = "parameters.json" )
        {
            // Update the field once the persistent path exists.
            saveFile = Application.persistentDataPath + "/" + filename;
            gameParameters = new GameParameters();
        }

        public void Load()
        {
            #if DEBUG
            Debug.Log("Loading parameters");
            #endif
            // Does the file exist?
            if (File.Exists(saveFile))
            {
            string fileContents = File.ReadAllText(saveFile);
            gameParameters = JsonUtility.FromJson<GameParameters>(fileContents);
            }
        }

        public void Save()
        {
            #if DEBUG
            Debug.Log("Saving parameters");
            #endif
            string json = JsonUtility.ToJson(gameParameters);
            File.WriteAllText(saveFile, json);
        }
    }
}