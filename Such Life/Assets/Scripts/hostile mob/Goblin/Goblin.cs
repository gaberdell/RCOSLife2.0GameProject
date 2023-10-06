using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MobBase
{
    public int maxHealth = 100; // Maximum health of the Goblin
    public int currentHealth; // Current health of the Goblin
    public int damage = 10; // Damage the Goblin deals to the player
    public float attackRange = 1.5f; // Range at which the Goblin can attack the player

    public Animator goblinAnimator; // Reference to the Goblin's animator component
    public SpriteRenderer goblinSpriteRenderer; // Reference to the Goblin's sprite renderer component

    public AudioClip stealSound; // Sound effect for stealing money
    public AudioClip defeatSound; // Sound effect for Goblin's defeat

    private bool canSteal = true; // Whether the Goblin can steal money
    public float stealCooldown = 3.0f; // Cooldown between steal actions

    public GameObject lootDropPrefab; // Prefab of the loot to drop when defeated

    public int runAwayMoneyThreshold = 50; // The amount of money at which the Goblin decides to run away
    private bool isRunningAway = false; // Indicates whether the Goblin is currently running away

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Initialize Goblin's health
        goblinAnimator = GetComponent<Animator>(); // Get the animator component
        goblinSpriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer component
        // Add other initialization code here
    }

    // Update is called once per frame
    void Update()
    {
        // Implement AI behavior, such as chasing the player, here
        // Check if the player is in attack range and attack if so
        if (distance < attackRange)
        {
            Attack();
        }

        // Check if the Goblin can steal money (cooldown)
        if (canSteal && distance < stealRange)
        {
            StealMoney();

            // Check if the stolen money reaches the threshold to run away
            if (moneyStolen >= runAwayMoneyThreshold)
            {
                if (!isRunningAway)
                {
                    RunAway();
                }
            }
        }
    }

    // Function to handle Goblin's attack
    void Attack()
    {
        // Check if the player is in range and able to be attacked
        if (player != null)
        {
            // Rotate the Goblin to face the player (optional)
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Deal damage to the player
            player.TakeDamage(damage);
        }
    }

    // Function to steal money from the player
    void StealMoney()
    {
        if (player.money >= stealAmount)
        {
            // Play steal sound effect
            AudioSource.PlayClipAtPoint(stealSound, transform.position);
            
            // Steal money from the player
            player.money -= stealAmount;
            moneyStolen += stealAmount;

            // Start the cooldown timer
            StartCoroutine(StealCooldown());
        }
        else
        {
            // If the player doesn't have enough money, steal the remaining amount
            moneyStolen += player.money;
            player.money = 0;

            // Start the cooldown timer
            StartCoroutine(StealCooldown());
        }

        Debug.Log("Goblin stole " + stealAmount + " money! Total stolen: " + moneyStolen);
    }

    // Cooldown timer for stealing
    IEnumerator StealCooldown()
    {
        canSteal = false;
        yield return new WaitForSeconds(stealCooldown);
        canSteal = true;
    }

    // Function to make the Goblin run away from the player
    void RunAway()
    {
        // Calculate the direction from the player to the Goblin
        Vector2 directionToPlayer = player.transform.position - transform.position;

        // Invert the direction to move away from the player
        Vector2 runDirection = -directionToPlayer.normalized;

        // Set the Goblin's position to move away from the player
        transform.position += runDirection * speed * Time.deltaTime;

        // Optionally, you can add animation or other behavior here to visually indicate running away

        // Set the running away flag to true
        isRunningAway = true;
    }
}

