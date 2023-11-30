using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Reason for this being in helper class is due to it needing to acess
//Item data
public interface IInventoryHolder
{
    public bool AddToPrimaryInventory(InventoryItemData inventoryItemData, int amount);
}
