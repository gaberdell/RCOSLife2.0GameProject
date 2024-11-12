using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    /*#region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;    
    }
    #endregion*/

    ItemType[] currentEquipment;

    [SerializeField]
    int numSlots = 8;

    [SerializeField]
    PlayerInventoryHolder playerInventoryHolder;

    InventorySystem playerInventory;

    [SerializeField]
    SpriteRenderer hatSprite;

    [SerializeField]
    //MERGE THIS STUFF INTO ONE SCRIPT!
    ArmorAnimator armorAnimator;

    bool isSetUpFunctionEnabled = false;

    private void Awake()
    {
        playerInventory = playerInventoryHolder.PrimaryInventorySystem;
    }

    private void OnEnable()
    { 
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += SetUpFunction;

        isSetUpFunctionEnabled = true;
    }

    //Wierdly by the time of OnEnable the event for 
    public void SetUpFunction(InventorySystem playerInventorySystem, int offset, int armorOffset)
    {
        if (isSetUpFunctionEnabled) {
            playerInventory.InventorySlots[10].itemSlotUpdated += UpdateSlotHead;
            PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= SetUpFunction;
            isSetUpFunctionEnabled = false;
        }
        
    }

    public void UpdateSlotHead()
    {
        armorAnimator.setItemToRender(playerInventory.InventorySlots[10].ItemData);
    }


    //Figure out a way to update the Equipment Sprite
    public void UpdateEquipmentSprite(InventorySlot updatedSlot)
    {
        for (int i = 0; i < playerInventory.InventorySlots.Count; i++)
        {
            if (Object.ReferenceEquals(updatedSlot, playerInventory.InventorySlots[i]) == true)
            {
                switch (i)
                {
                    case 10:
                        armorAnimator.setItemToRender(updatedSlot.ItemData);
                        break;
                    default:
                        break;
                }
            }

        }
    }


    //insert the newItem into the Equipment Array
    //NOTE: PLACEMENT MATTER because different element of the array is corresponding to diffrent slots
    /*public void Equip (Equipment newItem)
    {
        //ENUM will have give an index for all listed item.
        //ex: EquipmentSlot.Gloves = 0, EquipmentSlot.Helmet = 1, EquipmentSlot.Chestplate = 2, ...
        int slotIndex = (int)newItem.equipSlot;

        currentEquipment[slotIndex] = newItem;
    }*/
}
