using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slimmer : mobBase
{
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("MC").transform;

        monsterBody = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();
        agent.acceleration = 200;
        agent.stoppingDistance = 1f;
        agent.autoBraking = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currState = State.Wander;
        currPosition = new Vector2(transform.position.x, transform.position.y);

        maxHealth = 5;
        damage = 2;
        currHealth = maxHealth;
        playerSighted = false;
        time_move = 3.0f;
        
        time_move = 4f;
        time = 0f;
        distance = 0f;
        alertRange = 6.0f;
        knockbackDuration = 1;
        knockbackPower = 25;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("MC").transform;
        time += 1f * Time.deltaTime;
        distance = Vector2.Distance(target.position, currPosition);
        StateChange();
        if (time >= time_move)
        {
            time = 0f;
            if (currState == State.Chasing)
            {
                chasing(distance);
            }
            else if (currState == State.Wander)
            {
                wander();
            }
            agent.SetDestination(currPosition);
        }
        
    }

    void wander()
    {
        float oldPosX = currPosition.x;
        PositionChange();
        flipSprite(oldPosX);
    }

    void chasing(float distance)
    {
        float oldPosX = currPosition.x;
        angle = Mathf.Atan2((target.position.y - currPosition.y) , (target.position.x - currPosition.x));
        currPosition = new Vector2(currPosition.x + 2 * Mathf.Cos(angle), currPosition.y + 3 * Mathf.Sin(angle));
        flipSprite(oldPosX);
    }

    public override void PositionChange()
    { 
        float posxmin = transform.position.x - 1.0f;
        float posxmax = transform.position.x + 1.0f;
        float posymin = transform.position.y - 1.0f;
        float posymax = transform.position.y + 1.0f;

        currPosition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bounce();
        }
    }

    void StateChange()
    {
        if (distance <= alertRange && !playerSighted)
        {
            playerSighted = true;
            currState = State.Chasing;
            flipSprite(-target.position.x);
        }
    }

    void bounce()
    {
        float timer = 0;
        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 knockDirect = (target.transform.position - transform.position).normalized;
            monsterBody.AddForce(-knockDirect * knockbackPower);
        }
    } 

    void flipSprite(float PosX)
    {
        if (currPosition.x > PosX)
        {
            MobSprite.flipX = true;
        }
        else
        {
            MobSprite.flipX = false;
        }
    }

}
