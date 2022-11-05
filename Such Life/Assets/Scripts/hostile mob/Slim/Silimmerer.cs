using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ############################################
// #              USE INTERFACES              #
// ############################################

public class Slimmerer : Slimmer
{
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("MC").transform;

        monsterBody = GetComponent<Rigidbody2D>();
        monsterBody.drag = 15f;

        maxHealth = 5;
        damage = 2;
        currHealth = maxHealth;
        playerSighted = false;
        time_move = 3.0f;
        time_stop = 0.5f;

        speed = 3.5f;
        time = 0f;
        distance = 0f;
        alertRange = 6.0f;
        knockbackDuration = 0.7f;
        knockbackPower = 30;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = 200;
        agent.stoppingDistance = 1f;
        agent.autoBraking = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currState = State.Chasing;
        currPosition = new Vector2(transform.position.x, transform.position.y);
        prevPosition = currPosition;

    }

    // Update is called once per frame
    void Update()
    {
        currPosition = transform.position;

        target = GameObject.Find("MC").transform;
        time += 1f * Time.deltaTime;
        distance = Vector2.Distance(target.position, currPosition);
        StateChange();
        if (time >= time_move)
        {
            prevPosition = currPosition;
            agent.speed = speed;

            if (currState == State.Chasing)
            {
                agent.SetDestination(target.position);
            }
            else if (currState == State.Wander)
            {
                wander();
                agent.SetDestination(currPosition);
            }
        }
        if (time >= time_move + time_stop)
        {
            time = 0f;
            agent.speed = 0;
        }
    }

    // The slime can't see the player from meters away, it can only see
    // the player from alertRange units away. If the MC enters the alertRange,
    // the slime will begin attacking
    public override void StateChange()
    {
        if (distance <= alertRange && !playerSighted)
        {
            playerSighted = true;
            currState = State.Chasing;
            flipSprite(-target.position.x);
        }
        else
        {
            flipSprite(prevPosition.x);
        }
    }

}
