using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allow you to create an object of this type in the asset menu by right clicking
[CreateAssetMenu(fileName = "New Equipment", menuName = "Old Inventory System (Deprecated)/ArmorBase")]
public class ArmorBase : InventoryItemData
{
    //ArmorBase will only account for what is common within all of the armor piece. For individual equipment piece,
    // use prefab to create the possible main stats and other.
    // Boot, ring, and Dog Tag have a range of possible main stats
    // gloves, helmet, and chestplate have 1 possible main stat.

    /* To be implement:
     *  - Substats Rolling Range calculation
     *  - Substats increase probability
     *  - Armor Base Stat


    /* Equipment multiplier chart */
    //this array will store the required enhancement exp amount multiplier. The indicies will represent the equipment level.
    public float[] expMult;
    //this array will store the main stat multiplier. The indicies will represent the equipment level.
    public float[] mainStatMult;


    /* Equipment EXP */
    //Enhancement level
    public int equipmentLevel;
    //equipment's based EXP based on the rarity
    public int baseEXP;
    //the required exp amount to level up the piece of equipment
    //requiredEXP = baseEXP * expMultiplier[equipmentLevel];
    public float requiredEXP;
    //the current exp amount of the equipment
    public float currEXP;


    /* Equipment stats and sub-stats */
    //Gloves, Helmet, Chestplate, Boot, ... 
    public string armorType;
    //name of the set (Attack, Armor pen, Nimble,...
    public string setName;
    //for calculating the debuff based on the durability point
    public int durabilityPoint;
    public string rarity;
    // <String, int> ==> <substatsName, value>
    public Dictionary<string, int> substatsInfo = new Dictionary<string, int>();
    public int mainStats;
    //Attack, Def, %Def, Health, %Health, ...
    public string mainStatType;



    /* On creation of the scriptable object, run a "randomize rarity" and "randomize substats" function 
     * to populate the substatsInfo array depend on the rarity of the object. Once this
     * stat has been set for the object, you cannot change it.
     * The only substats that can be change/adjust after creation is mainStats (through enhancement)
     * and durabilityPoint (will go up and down when player repair the armor)
    */

    private void Awake()
    {
        //to be implement
    }
}
