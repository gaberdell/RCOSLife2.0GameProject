using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{//Nothing gets assigned until a specific type of weapon calls these variables.

    [SerializeField] int Durability;
    [SerializeField] string archetype; // more modular option later
    [SerializeField] string rarity; //Surplus,Common,Uncommon,Epic,Legendary,Mythic
    [SerializeField] int Piercing; //numEnemiesCanHit
    [SerializeField] float damage; //Amount of damage weapon is dealing
    [SerializeField] int range; //Number of tiles that the weapon can hit
    //Suggestion: range starting at 0 for melee?
    [SerializeField] int Capacity; //amount of projectile you can shoot before having to go through reload animation
    [SerializeField] int ReloadSpeed; //time it takes to replace the current “mag” with a new one
    [SerializeField] float attackSpeed; //Number of attacks per second
    [SerializeField] float FiringRate; //Rounds per minute
    //Suggestion: implement cap?
    [SerializeField] float armorPenetration; //Percentage of armor ignored
    double critDamage = 1.25; //Multiplier of the damage if crit is landed, weapon should add to that percentage
    float critChance = 0; //Percent chance to crit, weapon should add to this chance
    //string material; //Do not know how the specific weapons will inherit the class
    float weight; //How heavy the weapon, impacts how much player holds in inventory
    bool isOneHanded = false;
    bool isTwoHanded = false;//Determines if player can hold another weapon.
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