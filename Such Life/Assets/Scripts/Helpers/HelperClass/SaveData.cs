using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Base codes inspired by: Dan Pos - Game Dev Tutorials!*/


/*Small class that gets used by SaveGameManager to pass data to and fro
 *A copy of it gets stored in a jsonFile and a local one in SaveGameManager
 */
public class SaveData
{
    public List<GameObject> objectsToSave;
    public SerializableDictionary<string, SavableSlot[]> savedSlots;
    public SaveData()
    {
        objectsToSave = new List<GameObject>();
        savedSlots = new SerializableDictionary<string, SavableSlot[]>();
    }
}


