using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : mobBase
{
    
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
    public float bossSlamCooldown = 3.0f; // Adjust the cooldown value as needed
    public float lastSlamTime = 0.0f;

    public float knockbackForce = 5.0f;
    public LayerMask playerLayer; // Declare this in your class

   

    void Start()
    {
        maxHealth = 1000; // Extremely high health
        currentHealth = maxHealth;
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerLayer = playerObject.layer;
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
        // Replace this with your actual logic, for example:
        return Time.time - lastSlamTime >= bossSlamCooldown;
    }

    // Perform the slam attack
   void PerformSlamAttack()
    {
        // Check if the player is within slam range
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, meleeAttackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            // Calculate the knockback direction away from the boss
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

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


    void ShiftPhase()
    {
        if (currentPhase < totalPhases)
        {
            // Implement logic for transitioning to the next phase
            // For example, change boss behavior, appearance, or attack patterns

            // Increment the current phase
            currentPhase++;
        }
        else
        {
            // Handle the final phase or any other logic when all phases are completed
            // For example, you can call the Defeat method.
            Defeat();
        }
    }


    public void Defeat()
{
    // Implement boss defeat logic, such as:
    // - Dropping items or rewards
    // - Unlocking progression (e.g., opening a gate)
    // - Triggering a special achievement

    // For example, you can spawn a rare drop prefab:
    if (rareDropPrefab != null)
    {
        Instantiate(rareDropPrefab, transform.position, Quaternion.identity);
    }

    // You can also change the behavior or appearance of the boss to indicate defeat.
    // For example, you can disable the boss's AI or play a defeat animation.

    // Optionally, load the next area or perform other progression-related tasks.
    if (!string.IsNullOrEmpty(nextAreaName))
    {
        // Load the next area or trigger progression here.
        // SceneManager.LoadScene(nextAreaName); // If using Unity's SceneManager.
    }

    // Destroy or deactivate the boss GameObject.
    // You can also play a victory animation or display a victory screen.
    // For example:
    // gameObject.SetActive(false);
}

}