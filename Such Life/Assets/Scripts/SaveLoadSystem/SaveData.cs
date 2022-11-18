using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/


//This class will save whatever it is in your game
/* Things to save in your game: Player's position, Chest inventory data, player's backpack
    * Structure position, player's health and stats, all of the player's interaction,
    * NPC status and what they have been up to, NPC position,...
    * 
    * 
    */
public class SaveData
{
    //keep track of item that the player picks up
    public List<string> collectedItems;

    public SerializableDictionary<string, ItemPickUpSaveData> activeItems;
    public SerializableDictionary<string, InventorySaveData> chestDictionaryData;

    public InventorySaveData playerInventory;

    public SaveData()
    {
        collectedItems = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();
        chestDictionaryData = new SerializableDictionary<string, InventorySaveData>();
        playerInventory = new InventorySaveData();
    }
}


