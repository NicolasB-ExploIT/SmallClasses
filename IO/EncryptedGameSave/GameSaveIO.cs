using System.IO;
using UnityEngine;
using System.Security.Cryptography;

namespace SmallClasses.IO {
    public class GameSaveIO
    {
        byte[] savedKey;

        private string GetFileName(int saveId){
            return Application.persistentDataPath + "/game"+saveId.ToString()+".save";
        }

        public GameSave Load(int saveId)
        {
            #if DEBUG
            Debug.Log("Loading Save "+saveId.ToString());
            #endif

            string fileName = GetFileName(saveId);
            // Does the file exist?
            if (!File.Exists(fileName))
            {
                throw new System.Exception("Save not found");
            }

            // Pipe : StreamReader - CryptoStream - FileStream

            // Prepare IV
            Aes aes = Aes.Create();
            byte[] outputIV = new byte[aes.IV.Length];
            
            // Open file
            FileStream fileStream = new FileStream(fileName, FileMode.Open);

            // Read the IV unencrypted
            fileStream.Read(outputIV, 0, outputIV.Length);

            // Create a CryptoStream wrapping FileStream
            CryptoStream cryptoStream = new CryptoStream(
                    fileStream,
                    aes.CreateDecryptor(savedKey, outputIV),
                    CryptoStreamMode.Read);

            // Create a StreamReader wrapping CryptoStream
            StreamReader streamReader = new StreamReader(cryptoStream);
            
            // Get the entire file into a String
            string text = streamReader.ReadToEnd();
    
            // Close Streams
            streamReader.Close();
            cryptoStream.Close();
            fileStream.Close();

            // Deserialize the JSON data into a GameSave instance
            return JsonUtility.FromJson<GameSave>(text);
        }

        public void Save(GameSave gameSave, int saveId)
        {
            #if DEBUG
            Debug.Log("Saving Save "+saveId.ToString());
            #endif
            string fileName = GetFileName(saveId);

        // Pipe : StreamWriter - CryptoStream - FileStream
            
            // Prepare IV
            Aes aes = Aes.Create();
            savedKey = aes.Key;
            byte[] inputIV = aes.IV;

            // Create file
            FileStream fileStream = new FileStream(fileName, FileMode.Create);      
            
            // Write the IV unencrypted
            fileStream.Write(inputIV, 0, inputIV.Length);

            // Create a CryptoStream wrapping FileStream
            CryptoStream cryptoStream = new CryptoStream(
                    fileStream,
                    aes.CreateEncryptor(aes.Key, aes.IV),
                    CryptoStreamMode.Write);

            // Create a StreamWriter wrapping CryptoStream
            StreamWriter streamWriter = new StreamWriter(cryptoStream);

            // Serialize the save into JSON
            string jsonString = JsonUtility.ToJson(gameSave);

            // Write to the innermost stream (which will encrypt).
            streamWriter.Write(jsonString);

            // Close Streams
            streamWriter.Close();
            cryptoStream.Close();
            fileStream.Close();
        }
    }
}
