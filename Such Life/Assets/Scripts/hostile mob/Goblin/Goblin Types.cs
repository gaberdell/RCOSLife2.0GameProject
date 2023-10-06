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

