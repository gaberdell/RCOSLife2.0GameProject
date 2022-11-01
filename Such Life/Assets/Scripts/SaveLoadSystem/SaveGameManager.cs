using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/


namespace SaveLoadSystem
{
    public static class SaveGameManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        public const string SaveDirectory = "/SaveData/"; //folder that the save game is going into
        //public const string FileName = "SaveGame.sav"; //file name for the save
        public static bool SaveGame(string _fileName)
        {
            //          a "space" built into Unity
            var dir = Application.persistentDataPath + SaveDirectory;

            //If the directory does not exist, create a new directory folder
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string FileNameFormatted = _fileName + ".sav";
            string json = JsonUtility.ToJson(CurrentSaveData, true);

            //for "hardcoding" file name if needed
            //File.WriteAllText(dir + FileName, json);
            File.WriteAllText(dir + FileNameFormatted, json);


            //for "hardcoding" file name if needed
            //GUIUtility.systemCopyBuffer = dir;
            GUIUtility.systemCopyBuffer = dir;

            //Might put in other checks to fully check the state of the game before saving
            return true;
        }

        public static void LoadGame(string _fileName)
        {
            string FileNameFormatted = _fileName + ".sav";
            string fullPath = Application.persistentDataPath + SaveDirectory + FileNameFormatted;
            SaveData tempData = new SaveData();

            //If file exist
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json); //turn the json file back into the SaveData data type for usage
            }
            else
            {
                Debug.LogError("Save file does not exist!");
            }

            CurrentSaveData = tempData;
        }
    }
}
