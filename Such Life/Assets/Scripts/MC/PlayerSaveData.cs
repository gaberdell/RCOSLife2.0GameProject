using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/


public class PlayerSaveData : MonoBehaviour
{
    public float currHealth = 10f;
    private PlayerData myData = new PlayerData();
    // Update is called once per frame
    void Update()
    {
        //maybe set up like a ____ min saving interval
        myData.playerPos = transform.position;
        //myData.playerRotation = transform.position;
        myData.currentHealth = currHealth;

        //change it so that player can even press "save game" button to save or a shortcut key of choice to save
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SaveGameManager.CurrentSaveData.playerInfo = myData;
            SaveGameManager.SaveGame("testFile");
        }

        //change it so that player can even press "load game" button to save or a shortcut key of choice to load
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SaveGameManager.LoadGame("testFile");
            myData = SaveGameManager.CurrentSaveData.playerInfo;
            transform.position = myData.playerPos;
            currHealth = myData.currentHealth;
        }

    }
}

[System.Serializable]
//this struct will be what data should be save into the json files
public struct PlayerData 
{
    //Note: Add in info of player's inventory, chest inventory, NPC (later on)'s inventory and states, player's abilities, skill points
    //perks,...., and almost everything in the hierarchy.
    public Vector3 playerPos;
    //public Quaternion playerRotation (maybe keep track of the direction that the player is facing when exit the game)
    public float currentHealth;

}