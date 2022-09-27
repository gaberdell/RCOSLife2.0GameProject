using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slimmer : mobBase
{
    private float timeDelay;
    private float time;
    private float alertRange;
    public float distance;
    // Start is called before the first frame update
    public Transform target;
    public SpriteRenderer slimeSprite;
    
    void Start()
    {
        target = GameObject.Find("MC").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.acceleration = 200;
        agent.stoppingDistance = 1f;
        agent.autoBraking = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        maxHealth = 5;
        damage = 2;
        currHealth = maxHealth;
        walkSpeed = 1f;
        playerSighted = false;
        time_move = 3.0f;
        currPosition = new Vector2(transform.position.x, transform.position.y);

        timeDelay = 4f;
        time = 0f;
        distance = 0f;
        alertRange = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("MC").transform;
        time += 1f * Time.deltaTime;
        distance = Vector2.Distance(target.position, currPosition);
        if (distance <= alertRange && !playerSighted)
        {
            playerSighted = true;
            flipSprite(-target.position.x);
        }
        if (time >= timeDelay)
        {
            time = 0f;
            if (playerSighted)
            {
                chasing(distance);
            }
            else
            {
                wander();
            }
        }
    }

    void wander()
    {
        float oldPosX = currPosition.x;
        wanderPositionChange();
        flipSprite(oldPosX);
        agent.SetDestination(currPosition);
    }

    void chasing(float distance)
    {
        float oldPosX = currPosition.x;
        float angle = Mathf.Atan2((target.position.y - currPosition.y) , (target.position.x - currPosition.x));
        if (distance >= 1.8f)
        {
            this.currPosition = new Vector2(currPosition.x + 2 * Mathf.Cos(angle), currPosition.y + 2 * Mathf.Sin(angle));
        }
        else
        {
            this.currPosition = new Vector2(currPosition.x + 1 * Mathf.Cos(angle), currPosition.y + 1 * Mathf.Sin(angle));
        }
        flipSprite(oldPosX);
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

    void flipSprite(float PosX)
    {
        if (currPosition.x > PosX)
        {
            slimeSprite.flipX = true;
        }
        else
        {
            slimeSprite.flipX = false;
        }
    }

}
