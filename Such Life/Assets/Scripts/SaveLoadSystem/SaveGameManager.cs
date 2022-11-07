using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/



public static class SaveGameManager
{
    public static UnityAction onSaveGame;
    public static UnityAction<SaveData> onLoadGame;

    private const string SaveDirectory = "/SaveData/";

    //hardcode save file name :(
    private static string fileName = "SaveGame.sav";

    public static bool SaveGame(SaveData data)
    {
        onSaveGame?.Invoke();
        //          a "space" built into Unity
        string dir = Application.persistentDataPath + SaveDirectory;

        //If the directory does not exist, create a new directory folder
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(data, true);

        /* Later on, add such that the player can name their world
        and we can adjust the format of the file name to be:
        name_of_world.sav, where name_of_world is, you guess it, the name of
        the player's world
        */
        File.WriteAllText(dir + fileName, json);

        //return the path to save data for ease of debug
        GUIUtility.systemCopyBuffer = dir;

        //Might put in other checks to fully check the state of the game before saving
        return true;
    }

    public static SaveData LoadGame()
    {
        string fullPath = Application.persistentDataPath + SaveDirectory + fileName;
        SaveData tempData = new SaveData();

        //If file exist
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            tempData = JsonUtility.FromJson<SaveData>(json); //turn the json file back into the SaveData data type for usage

            onLoadGame?.Invoke(tempData);
        }
        else
        {
            Debug.LogError("Save file does not exist!");
        }

        return tempData;
    }
}

