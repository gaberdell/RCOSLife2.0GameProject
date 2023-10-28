using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : mobBase
{
    public int maxHealth = 1000; // Extremely high health
    public int currentHealth;
    public int defense = 50; // High defense

    public int sustainedDamageThreshold = 100; // Require sustained damage output to defeat
    private int sustainedDamageCounter = 0;

    public int meleeAttackDamage = 30; // Heavy damage melee attack
    public int rangedAttackDamage = 40; // Heavy damage ranged attack
    public int aoeAttackDamage = 60; // Heavy damage AoE attack

    public float meleeAttackRange = 2.0f;
    public float rangedAttackRange = 10.0f;
    public float aoeAttackRange = 5.0f;

    public GameObject meleeAttackEffectPrefab; // Prefab for melee attack effect
    public GameObject rangedAttackEffectPrefab; // Prefab for ranged attack effect
    public GameObject aoeAttackEffectPrefab; // Prefab for AoE attack effect

    public int totalPhases = 3; // Shift through multiple phases
    private int currentPhase = 1;

    public GameObject standardEnemyPrefab; // Prefab for additional standard enemies

    public GameObject rareDropPrefab; // Prefab for rare drops (crafting materials, recipes, cosmetics, etc)

    public string nextAreaName; // Name of the next area to unlock progression
    public string achievementName; // Name of the special achievement

    // // Add a reference to the player or player controller script
    // public PlayerController playerController;

       public float slamRange = 10.0f; // Range of the slam attack
    public float slamCooldown = 5.0f; // Cooldown between slam attacks
    private float lastSlamTime = 0;

    public Transform slamAttackPoint; // Point where the slam attack occurs
    public LayerMask playerLayer; // Layer to detect the player

    void Start()
    {
        currentHealth = maxHealth;
        // Other initialization code...
    }

    void Update()
    {
        if (CanPerformSlamAttack())
        {
            PerformSlamAttack();
        }

        // Implement other boss AI behavior, phases, and attacks...
    }

    // Check if the boss can perform a slam attack
    bool CanPerformSlamAttack()
    {
        return Time.time - lastSlamTime >= slamCooldown;
    }

    // Perform the slam attack
public float knockbackForce = 10.0f; // Adjust the force as needed

// Perform the slam attack
void PerformSlamAttack()
{
    // Check if the player is within slam range
    Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(slamAttackPoint.position, slamRange, playerLayer);

    foreach (Collider2D player in hitPlayers)
    {
        // Calculate the knockback direction away from the slam attack point
        Vector2 knockbackDirection = (player.transform.position - slamAttackPoint.position).normalized;

        // Apply a force to move the player slightly away
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    // Set the last slam attack time
    lastSlamTime = Time.time;
}



    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Defeat();
        }
    }

    void ShiftPhase()
    {
        // Implement phase shifting logic...
    }

    public void Defeat()
    {
        // Handle boss defeat logic, such as dropping items, unlocking progression, etc.
    }
}
