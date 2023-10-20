using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*The Zombie mob is a very simple mob that approaches the player when the player is within its range.
The Zombie is decently tanky and has high health, attack, and defense; it gets tankier when it falls below
half health. Its only method of attack is by touching the player*/
public class Zombie : mobBase
{

    // Start is called before the first frame update
    void Start()
    {

        maxHealth = 5000;
        currHealth = maxHealth;
        damage = 1000;
        critChance = 0.15f;
        critDamage = 1.5f;
        defense = 750;
        speed = 1f;
        player = GameObject.Find("MC");
        Sprite = GetComponent<SpriteRenderer>();
        navi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navi.updateRotation = false;
        navi.updateUpAxis = false;
        alertRange = 5.0f;
        currState = State.Idling;
        position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth <= maxHealth / 2)
        {
            defense = (int)(defense * 1.5); //Defense increases by 50% when health is below 50%. Rounds to the nearest whole number
        }
        if (currHealth <= 0)
        {
            currState = State.Dead;
        }

        if (currState != State.Dead)
        {
            if (findClosestObj("MC", alertRange) == player)
            {
                currState = State.Chasing;
                newposition = player.transform.position;
                navi.SetDestination(newposition);
            }
            else if (currState == State.Chasing)
            {
                navi.Stop();
                currState = State.Idling;
            }
            else
            {
                time = time + 1f * Time.deltaTime;
                if (time >= timeDelay)
                {
                    time = 0f;


                    //If the sheep is idling, it has a 50% chance to start wandering
                    if (currState == State.Idling)
                    {
                        int gen = Random.Range(0, 100);
                        if (gen > 40)
                        {
                            Wander();
                        }
                    }
                    if (currState == State.Wander)
                    {
                        PositionChange();
                        int gen = Random.Range(0, 100);
                        if (gen > 70)
                        {
                            Idle();
                        }
                    }
                }
            }
        }
    }
}