using UnityEngine;

public class StrongGoblin : Goblin
{
    // Override properties or methods to customize behavior for StrongGoblin
    public StrongGoblin()
    {
        maxHealth = 150; // Increase the max health
        damage = 20; // Increase the damage
    }
}

public class GreedyGoblin : Goblin
{
    // Override properties or methods to customize behavior for GreedyGoblin
    public GreedyGoblin()
    {
        stealAmount = 15; // Increase the amount of money stolen per steal action
        runAwayMoneyThreshold = 100; // Increase the threshold for running away
    }
}

 //   FastGoblin:

// FastGoblin.cs
public class FastGoblin : Goblin
{
    public FastGoblin()
    {
        speed = 8.0f; // Increase the speed
    }
}

//    TankGoblin:


// TankGoblin.cs

public class TankGoblin : Goblin
{
    public TankGoblin()
    {
        maxHealth = 200; // Increase the max health
        damage = 5; // Decrease the damage (but more health)
    }
}

//    NinjaGoblin:

// NinjaGoblin.cs

public class NinjaGoblin : Goblin
{
    public NinjaGoblin()
    {
        attackRange = 2.0f; // Increase the attack range
        stealAmount = 5; // Decrease the amount of money stolen per action
        runAwayMoneyThreshold = 20; // Lower the threshold for running away
    }

    // Override the RunAway method to make NinjaGoblin's escape more unique
    void RunAway()
    {
        // Implement NinjaGoblin's unique running away behavior
    }
}
