using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    /* Equipment multiplier chart */
    //this array will store the required enhancement exp amount multiplier. The indicies will represent the equipment level.
    public float[] expMult;
    //this array will store the main stat multiplier. The indicies will represent the equipment level.
    public float[] mainStatMult;


    /* Equipment rarity and stats */
    //This string array will store all of the rarity. Any value that depend on the rarity will use the indexes correspond to the rarity_info
    //0: Surplus
    //1: Common
    //2: Uncommon
    //3: Epic
    //4: Legendary
    //5: Mythic
    public string[] rarityInfo = { "Surplus", "Common", "Uncommon", "Epic", "Legendary", "Mythic" };
    //Left side gear will only have flat mainStats and subStats
    public string[] leftSideGear = { "Gloves", "Helmet", "Chestplate" };
    public string[] mainStatTypeLeft = { "Attack", "Defense", "Health"};

    //Right side gear will have both flat and % mainStats and subStats
    public string[] rightSideGear = { "Boot", "Ring", "Dog Tags"};
    public string[] mainStatTypeRight = { "Attack", "Defense", "Health", "Attack%", "Defense%", "Health%", "Effect Res%", "Effectiveness%", "Crit%", "CDmg%"};


    public int[] healthStat = { 50, 50, 150, 250, 300, 400};
    public int[] defenseStat = { 25, 50, 50, 75, 75, 100};
    public int[] attackStat = { 25, 25, 50, 50, 75, 100};
    public int[] percentageStat = { 6, 7, 7, 8, 9, 10};



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

    public EquipmentSlot equipSlot;


    // Constructor
    // gearSide will be representing either the left side gear or the right side gear.
    public Equipment(int mainStatVal, string equipmentType, string setType, int equipmentRarity, string gearSide)
    {
        armorType = equipmentType;
        mainStats = mainStatVal;
        setName = setType;
        rarity = rarityInfo[equipmentRarity];





        // and all the other functions like date calculator.
    }

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
