using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class ArmorAnimator : MonoBehaviour
{
    public bool facingNorth;
    public bool facingEast;
    public bool facingSouth;
    public bool facingWest;

    public InventoryItemData itemToRender;


    [SerializeField]
    SpriteRenderer spriteToChange;


    public void setItemToRender(InventoryItemData itemData)
    {
        itemToRender = itemData;

        spriteToChange.sprite = spriteToShow(itemData);
        //spriteToChange = itemData;
    }

    private Sprite spriteToShow(InventoryItemData itemData)
    {
        if (itemData)
        {
            return spriteToUpdate(itemData);
        }
        else
            return null;
    }

    private Sprite spriteToUpdate(InventoryItemData itemData)
    {
        EquipmentData equipData = itemData.equipmentConnection;
        if (equipData)
        {
            Debug.Log("sus");
            //Sorry i hate this but idk what else to do :skull:
            if (facingNorth)
            {
                Debug.Log("sus2");
                if (facingEast)
                    return equipData.northEastSprite;
                else if (facingWest)
                    return equipData.northWestSprite;

                return equipData.northSprite;
            }
            else if (facingSouth)
            {
                if (facingEast)
                    return equipData.southEastSprite;
                else if (facingWest)
                    return equipData.southWestSprite;

                return equipData.southSprite;
            }
            else if (facingWest)
                return equipData.westSprite;
            else if (facingEast)
                return equipData.eastSprite;
        }

        return itemData.Icon;
    }

    void Update()
    {
        if (itemToRender && itemToRender.equipmentConnection)
        {
            spriteToChange.sprite = spriteToUpdate(itemToRender);
        }
    }
}
