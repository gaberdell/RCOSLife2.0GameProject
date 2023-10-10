using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MeleeWeapon : MonoBehaviour
{//Nothing gets assigned until a specific type of weapon calls these variables.

    [SerializeField] int Durability;
    [SerializeField] string archetype; // more modular option later
    [SerializeField] string rarity; //Surplus,Common,Uncommon,Epic,Legendary,Mythic
    [SerializeField] float damage; //Amount of damage weapon is dealing
    [SerializeField] int range; //Number of tiles that the weapon can hit
    //Suggestion: range starting at 0 for melee?
    [SerializeField] float attackSpeed; //Number of attacks per second
    //Suggestion: implement cap?
    [SerializeField] float armorPenetration; //Percentage of armor ignored
    double critDamage = 1.25; //Multiplier of the damage if crit is landed, weapon should add to that percentage
    float critChance = 0; //Percent chance to crit, weapon should add to this chance
    //string material; //Do not know how the specific weapons will inherit the class
    float weight; //How heavy the weapon, impacts how much player holds in inventory
    bool isOneHanded = false;
    bool isTwoHanded = false;//Determines if player can hold another weapon.
}
