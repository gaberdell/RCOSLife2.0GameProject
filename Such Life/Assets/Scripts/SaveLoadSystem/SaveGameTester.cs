using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameTester : MonoBehaviour
{
    public void SaveGame()
    {
        SaveGameManager.CurrentSaveData.index = 10;
        //adjust so that the user can save their custom world name and be able to load that same world name instead of just "testFile"
        SaveGameManager.SaveGame("testFile");
    }

    public void LoadGame()
    {
        SaveGameManager.CurrentSaveData.index = 20;
        //adjust so that the user can save their custom world name and be able to load that same world name instead of just "testFile"
        SaveGameManager.LoadGame("testFile");
        Debug.Log(SaveGameManager.CurrentSaveData.index);
    }
}

