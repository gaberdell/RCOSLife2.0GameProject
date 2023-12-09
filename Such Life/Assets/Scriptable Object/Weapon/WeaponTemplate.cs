using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "WeaponTemplateDoNotCreate")]
public class Weapon : ScriptableObject
{//Nothing gets assigned until a specific type of weapon calls these variables.

    // All weapon stats have in common
    [SerializeField] int MAttack; //Melee
    [SerializeField] int RAttack; //Range
    [SerializeField] string Rarity; //Surplus,Common,Uncommon,Epic,Legendary,Mythic
    [SerializeField] int Reinforce; //Overflow repair will be converted into this bar. Boost weapon’s damage when this bar has a value that is bigger than 0
    [SerializeField] Archetype WArchetype; // Declared in Archetype ScriptableObject

    //Melee


    //Range
    [SerializeField] int Pierce; //numEnemiesCanHit
    [SerializeField] float FiringRate; //Rounds per minute
    [SerializeField] int Capacity; //amount of projectile you can shoot before having to go through reload animation
    [SerializeField] int ReloadSpeed; //time it takes to replace the current “mag” with a new one

    public void constructor()
    {
        int index;
        int RandomNum = Random.Range(1,201);
        string[] Rarities = {"Surplus","Common","Uncommon","Epic","Legendary","Mythic"};
        Dictionary<string, int[]> Attacks = new Dictionary<string, int[]>() {
            { "Surplus", new int[] { 50, 25 } },
            { "Common", new int[] { 50, 25 } },
            { "Uncommon", new int[] { 75, 50 } },
            { "Epic", new int[] { 75, 50 } },
            { "Legendary", new int[] { 100, 75 } },
            { "Mythic", new int[] { 200, 100 } }
        };  // index 0 Melee Attack, index 1 Range Attack
        if(RandomNum <= 100)
            index = 0;
        else if(RandomNum <= 170)
            index = 1;
        else if(RandomNum <= 194)
            index = 2;
        else if(RandomNum <= 199)
            index = 3;
        else
            index = 4;
        Rarity = Rarities[index];
        MAttack = Attacks[Rarity][0];
        RAttack = Attacks[Rarity][1];
    }
    //Suggestion: range starting at 0 for melee?

    //Hybrid


    //serialize reference
    //[field: SerializeReference] public List<ComponentData> ComponentData



    //Suggestion: implement cap?
    //[SerializeField] int Range; //Number of tiles that the weapon can hit
    //[SerializeField] float Damage; //Amount of damage weapon is dealing
    //[SerializeField] float ArmorPenetration; //Percentage of armor ignored
    double critDamage = 1.25; //Multiplier of the damage if crit is landed, weapon should add to that percentage
    float critChance = 0; //Percent chance to crit, weapon should add to this chance
}
//Change this so it uses Get Setters as opposed to this
public abstract class ItemWeapon {//Nothing gets assigned until a specific type of weapon calls these variables.

    int numEnemiesCanHit; //Piercing
    float damage; //Amount of damage weapon is dealing
    int range; //Number of tiles that the weapon can hit
    //Suggestion: range starting at 0 for melee?
    float attackSpeed; //Number of attacks per second
    //Suggestion: implement cap?
    float armorPenetration; //Percentage of armor ignored
    double critDamage; //Multiplier of the damage if crit is landed, weapon should add to that percentage
    float critChance; //Percent chance to crit, weapon should add to this chance
    //string material; //Do not know how the specific weapons will inherit the class
    float weight; //How heavy the weapon, impacts how much player holds in inventory
    bool isOneHanded;
    bool isTwoHanded;//Determines if player can hold another weapon.

}

/* Example:
class Axe : Weapon {
    numEnemiesCanHit = 1;
    range = 1;
    attackSpeed = 1.0;
    armorPenetration = 0.0;
    //critDamage = 0.0;
    isOneHanded = true;
}

class WoodAxe : Axe {
    damage = 1.0;
    weight = 1.0;
}

class StoneAxe : Axe {
    damage = 2.0;
    weight = 2.0;
}

class IronAxe : Axe {
    damage = 3.0;
    weight = 3.0;
}

class WoodPickaxe : Axe {
    damage = 1.0;
    weight = 1.0;
}

class StonePickaxe : Axe {
    damage = 2.0;
    weight = 2.0;
}

class IronPickaxe : Axe {
    damage = 3.0;
    weight = 3.0;
}

class Arrow : Weapon {
    numEnemiesCanHit = 1;
    range = 1;//This is really the range of the bow but am putting it here for now
    attackSpeed = 1.5;//Same as range
    isTwoHanded = true;//Same as range
    armorPenetration = .05;
    critChance = .05;
}

class WoodArrow : Arrow {
    damage = 1.0;
    weight = 1.0;
}

class StoneArrow : Arrow {
    damage = 2.0;
    weight = 2.0;
}

class IronArrow : Arrow {
    damage = 3.0;
    weight = 3.0;
}

class Scythe : Weapon {
    numEnemiesCanHit = 2;
    range = 3;
    attackSpeed = .5;
    isTwoHanded = true;
    armorPenetration = 0.0;
    critChance += 0.0;
}

class WoodScythe : Scythe {
    damage = 2.0;
    weight = 1.0;
}

class StoneScythe : Scythe {
    damage = 4.0;
    weight = 2.0;
}

class IronScythe : Scythe {
    damage = 8.0;
    weight = 3.0;
}
*/