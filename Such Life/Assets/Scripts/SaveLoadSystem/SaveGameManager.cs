using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/

/* Due note go through the EventManager when saving and loading
 * but this is the main thing that it calls to help due its stuff
 * like when they have to save or load. Think of it like the save 
 * load api for the game!
 * 
 * (Note other scripts can go through it but its not recomeneded)
 * NOTE: it has some help from SaveLoad and SaveData */
public class SaveGameManager : MonoBehaviour
{
    //We have a local data save so we only have to load data once then
    //use this local copy as opposed to keep on loading from a json file
    public static SaveData dataSingleton;

    private void Awake()
    {
        dataSingleton = new SaveData();
        dataSingleton = SaveLoad.Load();
    }

    private void OnEnable()
    {
        EventManager.onSaveGame += SaveGame;
        EventManager.onDeleteData += DeleteData;
        EventManager.onLoadGame += LoadData;
    }

    private void OnDisable()
    {
        EventManager.onSaveGame -= SaveGame;
        EventManager.onDeleteData -= DeleteData;
        EventManager.onLoadGame -= LoadData;
    }

    private void SoftSaveSlot(string ID, SavableSlot[] slots)
    {
        dataSingleton.savedSlots.Add(ID, slots);
    }

    private void DeleteData()
    {
        SaveLoad.DeleteSaveData();
        dataSingleton = new SaveData();
    }
    private static bool SaveGame(SaveData saveData)
    {
        dataSingleton = saveData;
        return SaveLoad.Save(saveData);
    }
    private static SaveData LoadData()
    {
        return dataSingleton;
    }
    
    //Probably will go unused if you want to grab the data from the
    //JSON file idk why you want to do that tho
    private static SaveData HardLoadData()
    {
        return SaveLoad.Load();
    }
}
