//Change this so it uses Get Setters as opposed to this
public abstract class Weapon {//Nothing gets assigned until a specific type of weapon calls these variables.

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