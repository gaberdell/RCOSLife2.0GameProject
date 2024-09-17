using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Old Inventory System (Deprecated)/Equipment")]
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
    //Left side gear will only have flat mainStats, with the indexes correspond to the stat type
    // Gloves should only have Attack; Helmet should only have Defense; Chestplate should only have "Health"
    public string[] leftSideGear = { "Gloves", "Helmet", "Chestplate" };
    public string[] mainStatTypeLeft = { "Attack", "Defense", "Health" };

    //Right side gear will have both flat and % mainStats
    public string[] rightSideGear = { "Boot", "Ring", "Dog Tags" };
    //This distionary will store all of the possible substats for all 3 of armor type
    //0: Boot
    //1: Ring
    //2: Dog Tags
    public Dictionary<int, string[]> possibleMainStatTypeRight = new Dictionary<int, string[]>()
    {
        { 0, new string[] { "Attack", "Defense", "Health", "Attack%", "Defense%", "Health%", "Effect Res%", "Effectiveness%" } },
        { 1, new string[] { "Attack", "Defense", "Health", "Attack%", "Defense%", "Health%", "Effect Res%", "Effectiveness%" } },
        { 2, new string[] {"Attack", "Defense", "Health", "Attack%", "Defense%", "Health%", "Effect Res%", "Effectiveness%", "Crit%", "CDmg%"} }
    };


    public int[] flatHealthStat = { 50, 50, 150, 250, 300, 400};
    public int[] flatDefenseStat = { 25, 50, 50, 75, 75, 100};
    public int[] flatAttackStat = { 25, 25, 50, 50, 75, 100};
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
    public int mainStatsVal;
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
    /* equipmentType: 0, 1, 2 (represent the index for rightSideGear or leftSideGear)
     * setType: Attack, Health, Defense, Reflection, Nimble,.... (Refer to the Wiki)
     * equipmentRarity: "Surplus", "Common", "Uncommon", "Epic", "Legendary", "Mythic"
     * gearSide: "leftSide" or "rightSide" strings only
     * 
     */
    public Equipment(int equipmentType, string setType, int equipmentRarity, string gearSide)
    {
        setName = setType;
        rarity = rarityInfo[equipmentRarity];

        //if the gear is left side gear, they only have flat stats (refer to line 25 in this script)
        //or else it would be the rightSide gear, which can have all of the stats store in mainStatTypeRight 
        if (gearSide == "leftSide")
        {
            //gear type
            armorType = leftSideGear[equipmentType];
            //main stat type
            mainStatType = mainStatTypeLeft[equipmentType];
            //determine the main stats value based on the rarity
            if (mainStatType == "Attack")
            {
                mainStatsVal = flatAttackStat[equipmentRarity];
            } 
            else if (mainStatType == "Defense")
            {
                mainStatsVal = flatDefenseStat[equipmentRarity];
            }
            else if (mainStatType == "Health")
            {
                mainStatsVal = flatHealthStat[equipmentRarity];
            }
        }
        else
        {
            //gear type
            armorType = rightSideGear[equipmentType];
            //randomized main stats by using the indexes
            int randomStatIndex = Random.Range(0, possibleMainStatTypeRight[equipmentType].Length); //get a random index number for equipment type
            //main stat type
            mainStatType = possibleMainStatTypeRight[equipmentType][randomStatIndex];
            if (mainStatType == "Attack")
            {
                mainStatsVal = flatAttackStat[equipmentRarity];
            }
            else if (mainStatType == "Defense")
            {
                mainStatsVal = flatDefenseStat[equipmentRarity];
            }
            else if (mainStatType == "Health")
            {
                mainStatsVal = flatHealthStat[equipmentRarity];
            }
            else
            {
                mainStatsVal = percentageStat[equipmentRarity];
            }
        }




        
    }

    public override void Use()
    {
        base.Use();
        // equip the equipment
        // remove it from inventory if all of the amount got use up or subtract the amount by 1
        RemoveFromInventory();
    }

}

public enum EquipmentSlot { Gloves, Helmet, Chestplate, Boots, Ring, dogTag}
