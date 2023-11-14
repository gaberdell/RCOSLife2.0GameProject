using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    // maybe add in more info
    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        // equip the equipment
        EquipmentManager.instance.Equip(this);
        // remove it from inventory if all of the amount got use up or subtract the amount by 1
        RemoveFromInventory();
        //(To be implement later)
    }

}

public enum EquipmentSlot { Gloves, Helmet, Chestplate, Boots, Ring, dogTag}
