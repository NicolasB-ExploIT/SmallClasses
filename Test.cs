using UnityEngine;
using UnityEngine.Assertions;
using SmallClasses.Audio;
using SmallClasses.Audio.Scriptable;
using SmallClasses.IO;

public class Test : MonoBehaviour
{
    [SerializeField] private PlayList myPlayList;
    [SerializeField] private SoundBank mySoundBank;
    private GameSaveIO saveIO;
    private GameParametersIO parametersIO;
    // Start is called before the first frame update
    void Start()
    {
       
        #if DEBUG
        Debug.Log("Path : "+Application.persistentDataPath);
        #endif
    
        //Test Music PlayList
        if(myPlayList != null) {
            MusicPlayerSingleton.volume = 1.0f;
            Music music = myPlayList.First();
            MusicPlayerSingleton.Play(music.audioClip);
            Debug.Log($"Playing : {music.title} by {music.credits} ({music.details})");
            
        }

        //Test Sound Bank
        if(mySoundBank != null) {
            Sound sound = mySoundBank.GetAt(0);
            SoundPlayerSingleton.volume = 0.4f;
            SoundPlayerSingleton.Play(sound.audioClip, sound.volume);
            Debug.Log($"Sound : {sound.description} ");
        }
        

        //Test GameParameters
        parametersIO = new GameParametersIO(); 
        GameParameters gp = new GameParameters();
        gp.highScore = 999;
        gp.lives = 2;
        gp.playerName = "Our Test";

        parametersIO.gameParameters = gp;
        parametersIO.Save();
        parametersIO.Load();
        Assert.AreEqual(999, parametersIO.gameParameters.highScore);
        Assert.AreEqual("Our Test", parametersIO.gameParameters.playerName);
        Assert.AreEqual(2, parametersIO.gameParameters.lives);
         #if DEBUG
        Debug.Log("GameParameters OK");
        #endif

        //Test GameSave
        saveIO = new GameSaveIO();
        GameSave gs = new GameSave();
        gs.highScore = 111;
        gs.lives = 4;
        gs.playerName = "Save Test\n<br />It's Cool.";

        saveIO.Save(gs, 1);
        GameSave anotherGs = saveIO.Load(1);
        Assert.AreEqual(111, anotherGs.highScore);
        Assert.AreEqual("Save Test\n<br />It's Cool.", anotherGs.playerName);
        Assert.AreEqual(4, anotherGs.lives);

        #if DEBUG
        Debug.Log("GameSave OK");
        #endif

        //Test Sound Bank
        if(mySoundBank != null) {
            mySoundBank.Next();
            mySoundBank.Previous();
            Sound sound = mySoundBank.Current();
            SoundPlayerSingleton.volume = 0.4f;
            SoundPlayerSingleton.Play(sound.audioClip, sound.volume);
            Debug.Log($"Sound : {sound.description} ");
        }

        //Test Music PlayList
        if(myPlayList != null) {
            MusicPlayerSingleton.volume = 1.0f;
            Music music = myPlayList.Last();
            MusicPlayerSingleton.Play(music.audioClip);
            Debug.Log($"Playing : {music.title} by {music.credits} ({music.details})");
            
        }

        #if DEBUG
        Debug.Log("All Tests Done");
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
