using System.Collections;
using System.Collections.Generic;
public class Goblin : MobBase
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public float attackRange = 1.5f;

    public GameObject healthBarPrefab; // Prefab for the health bar UI
    public GameObject combatEffectPrefab; // Prefab for combat effects (e.g., particles)
    
    public Transform[] patrolWaypoints; // Waypoints for patrolling
    public float patrolSpeed = 2.0f;
    private int currentWaypointIndex = 0;

    private GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;

        // Instantiate and attach a health bar to the Goblin
        GameObject healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBarInstance.transform.SetParent(transform);

        // Get a reference to the GameManager
        gameManager = FindObjectOfType<GameManager>();

        // Start patrolling if there are waypoints
        if (patrolWaypoints.Length > 0)
        {
            Patrol();
        }
    }

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
    
    void Patrol()
    {
        if (patrolWaypoints.Length > 0)
        {
            Vector3 targetWaypoint = patrolWaypoints[currentWaypointIndex].position;
            Vector3 moveDirection = (targetWaypoint - transform.position).normalized;

            // Move towards the current waypoint
            transform.position += moveDirection * patrolSpeed * Time.deltaTime;

            // If Goblin reaches the waypoint, move to the next waypoint
            if (Vector3.Distance(transform.position, targetWaypoint) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
            }
        }
    }

    public void Defeat()
    {
        // Play defeat sound effect...
        
        // Instantiate combat effects (e.g., particles)
        Instantiate(combatEffectPrefab, transform.position, Quaternion.identity);

        // Drop randomized loot
        gameManager.DropRandomLoot(transform.position);

        // Destroy the Goblin game object
        Destroy(gameObject);
    }
}

