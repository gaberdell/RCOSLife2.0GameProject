using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/

/*Alright so this script is what most others scrips go through
 when they have to save or load. Think of it like the save 
 load api for the game!
 *NOTE: it has some help from SaveLoad and SaveData */
public class SaveGameManager : MonoBehaviour
{
    public static SaveData data;

    private void Awake()
    {
        data = new SaveData();
        SaveLoad.onLoadGame += LoadData;     
    }

    private void OnEnable()
    {
        EventManager.onSaveGame += SaveData;
        EventManager.onDeleteData += DeleteData;
    }

    private void OnDisable()
    {
        EventManager.onSaveGame -= SaveData;
        EventManager.onDeleteData -= DeleteData;
    }

    public void DeleteData()
    {
        SaveLoad.DeleteSaveData();
    }

    private static void SaveData()
    {
        var saveData = data;
        SaveLoad.Save(saveData);
    }
    public static void LoadData(SaveData _data)
    {
        data = _data;
    }

    public static void TryLoadData()
    {
        SaveLoad.Load();
    }
}
