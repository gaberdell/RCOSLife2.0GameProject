using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MobBase
{
    public int moneyStolen = 0; // Track the money stolen by the Goblin
    public int stealAmount = 10; // Amount of money to steal per steal action

    // Start is called before the first frame update
    void Start()
    {
        // Initialize any Goblin-specific variables or behaviors here
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the Goblin and the player
        distance = Vector2.Distance(transform.position, player.transform.position);

        // Calculate the direction from the Goblin to the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Move the Goblin towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        // Check if the Goblin is close enough to the player to steal money
        if (distance < stealRange)
        {
            StealMoney();
        }
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate()
    {
        // Implement any FixedUpdate logic here if needed
    }

    // Function to steal money from the player
    void StealMoney()
    {
        if (player.money >= stealAmount)
        {
            // Steal money from the player
            player.money -= stealAmount;
            moneyStolen += stealAmount;

            Debug.Log("Goblin stole " + stealAmount + " money! Total stolen: " + moneyStolen);
        }
        else
        {
            // If the player doesn't have enough money, steal the remaining amount
            moneyStolen += player.money;
            player.money = 0;

            Debug.Log("Goblin stole all remaining money! Total stolen: " + moneyStolen);
        }
    }
}

