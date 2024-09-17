using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Covers some stuff unique to equipment like its visuals when equiped
[CreateAssetMenu(menuName = "Inventory System/Equipment Data")]
public class EquipmentData : ScriptableObject
{
    public Sprite northSprite;
    public Sprite northEastSprite;
    public Sprite eastSprite;
    public Sprite southEastSprite;
    public Sprite southSprite;
    public Sprite southWestSprite;
    public Sprite westSprite;
    public Sprite northWestSprite;
}
