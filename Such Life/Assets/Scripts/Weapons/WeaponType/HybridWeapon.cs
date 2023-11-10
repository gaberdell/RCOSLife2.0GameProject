using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HybridWeapon", menuName = "HybridWeapon")]
public class HybridWeapon : ScriptableObject
{//Nothing gets assigned until a specific type of weapon calls these variables.

    // All weapon stats have in common
    [SerializeField] string WeaponName;
    [SerializeField] Rarity WRarity; //Surplus,Common,Uncommon,Epic,Legendary,Mythic
    [SerializeField] int Reinforce; //Overflow repair will be converted into this bar. Boost weapon’s damage when this bar has a value that is bigger than 0
    [SerializeField] Archetype WArchetype; // Declared in Archetype ScriptableObject

    [SerializeField] int Pierce; //numEnemiesCanHit
    [SerializeField] float FiringRate; //Rounds per minute
    [SerializeField] int Capacity; //amount of projectile you can shoot before having to go through reload animation
    [SerializeField] int ReloadSpeed; //time it takes to replace the current “mag” with a new one

    //[SerializeField] int Range; //Number of tiles that the weapon can hit
    //[SerializeField] float Damage; //Amount of damage weapon is dealing
    //[SerializeField] float ArmorPenetration; //Percentage of armor ignored
    double critDamage = 1.25; //Multiplier of the damage if crit is landed, weapon should add to that percentage
    float critChance = 0; //Percent chance to crit, weapon should add to this chance
    //string material; //Do not know how the specific weapons will inherit the class
    float weight; //How heavy the weapon, impacts how much player holds in inventory
    bool isOneHanded = false;
    bool isTwoHanded = false;//Determines if player can hold another weapon.
}
