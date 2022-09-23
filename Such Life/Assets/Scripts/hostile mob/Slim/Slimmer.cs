using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slimmer : mobBase
{
    private float timeDelay = 90f;
    private float time = 0f;
    // Start is called before the first frame update
    public Transform target;
    public NavMeshAgent agent;
    public SpriteRenderer slimeSprite;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        maxHealth = 5;
        damage = 2;
        currHealth = maxHealth;
        walkSpeed = 1f;
        playerSighted = false;
        time_move = 3.0f;
        currPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        time += 1f;
        if (time >= timeDelay)
        {
            time = 0;
            wander();
        }
    }

    void wander()
    {
        float oldPos = currPosition[0];
        wanderPositionChange();
        if (currPosition[0] > oldPos)
        {
            slimeSprite.flipX = true;
        }
        else
        {
            slimeSprite.flipX = false;
        }
        agent.SetDestination(currPosition);
    }

    void wanderPositionChange()
    { 
        posxmin = transform.position.x - 1.0f;
        posxmax = transform.position.x + 1.0f;
        posymin = transform.position.y - 1.0f;
        posymax = transform.position.y + 1.0f;

        this.currPosition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
    }

}
