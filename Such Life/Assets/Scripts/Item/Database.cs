using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



[CreateAssetMenu (menuName = "Inventory System / Item Database")]
public class Database : ScriptableObject
{
    [SerializeField] private List<InventoryItemData> itemDatabase;
    
    //set an ID for an item so each item will have an un-chaning ID
    [ContextMenu("Set IDs")]
    public void SetItemIDs()
    {
        itemDatabase = new List<InventoryItemData>();

        //load all ItemData scriptable objects, sort and then put them into founditems
        //i here is for "item"
        var foundItems = Resources.LoadAll<InventoryItemData>("ItemData").OrderBy(i => i.ID).ToList();

        //only get call once when we build the game
        var hasIDInRange = foundItems.Where(i => i.ID != -1 && i.ID < foundItems.Count).OrderBy(i => i.ID).ToList();
        var hasIDNotInRange = foundItems.Where(i => i.ID != -1 && i.ID >= foundItems.Count).OrderBy(i => i.ID).ToList(); ;
        var noID = foundItems.Where(i => i.ID <= -1).ToList();

        var tempIndex = 0;
        for (int i = 0; i < foundItems.Count; i++)
        {
            InventoryItemData itemToAdd;
            //d here is for "data"
            itemToAdd = hasIDInRange.Find(d => d.ID == i);

            //if item DNE, add to database
            if(itemToAdd != null)
            {
                itemDatabase.Add(itemToAdd);
            }
            else if (tempIndex < noID.Count)
            {
                noID[tempIndex].ID = i;
                itemToAdd = noID[tempIndex];
                tempIndex++;
                itemDatabase.Add(itemToAdd);
            }
        }

        //prevent changing item's ID if there is another save files
        foreach (var item in hasIDNotInRange)
        {
            itemDatabase.Add(item);
        }
    }



    public InventoryItemData GetItem (int id)
    {
        return itemDatabase.Find(i => i.ID == id);
    }


}
