using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;    
    }
    #endregion

    Equipment[] currentEquipment;

    void Start()
    {
        // This is a string array of all of the equipment in the EquipmentSlot.
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    //insert the newItem into the Equipment Array
    //NOTE: PLACEMENT MATTER because different element of the array is corresponding to diffrent slots
    public void Equip (Equipment newItem)
    {
        //ENUM will have give an index for all listed item.
        //ex: EquipmentSlot.Gloves = 0, EquipmentSlot.Helmet = 1, EquipmentSlot.Chestplate = 2, ...
        int slotIndex = (int)newItem.equipSlot;

        currentEquipment[slotIndex] = newItem;
    }
}
