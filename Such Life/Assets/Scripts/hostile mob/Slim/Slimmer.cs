using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slimmer : mobBase
{
    public GameObject slimeSplitter = null;
    public float time_stop;
    public Vector2 prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("MC").transform;
        MobSprite = GetComponent<SpriteRenderer>();

        monsterBody = GetComponent<Rigidbody2D>();
        monsterBody.drag = 15f;

        maxHealth = 5;
        currHealth = maxHealth;
        damage = 2;
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

        currState = State.Wander;
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

    // Wandering. The slime will walk in random directions
    public void wander()
    {
        PositionChange();
    }
   

    // PositionChange() is a help function for wander(). It grabs random x and y values and puts them into
    // an attriibute called currPosition. After SetDestination is called, the slime will go the location
    // represented by currPosition
    public override void PositionChange()
    { 
        float posxmin = transform.position.x - 1.0f;
        float posxmax = transform.position.x + 1.0f;
        float posymin = transform.position.y - 1.0f;
        float posymax = transform.position.y + 1.0f;

        currPosition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
    }

    // A built in Unity collision detector. It will perform anything inside this function if the slime collides with something.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // If it collides with the player it will bounce off.
        if (collision.gameObject.tag == "Player")
        {
            bounce();
        }
        // We don't want the slime to jump as it's bouncing back.
        // Setting the time to zero resets the Update timer.
        time = 0;
    }

    // The slime can't see the player from meters away, it can only see
    // the player from alertRange units away. If the MC enters the alertRange,
    // the slime will begin attacking
    virtual public void StateChange()
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
        if (currHealth <= 0)
        {
            // Oof
            Destroy(this.gameObject);

            if (slimeSplitter)
            {
                // Split up
                Instantiate(slimeSplitter, transform.position + transform.forward * 2, transform.rotation);
                Instantiate(slimeSplitter, transform.position + transform.right * 2, transform.rotation);
            }
        }
    }

    // The function used to implement bounce
    public void bounce()
    {
        // A separate timer for bouncing
        float bounce_time = 0;
        // Knockback duration has to be fixed, otherwise the slime will drag itself back to
        // the position it was at before it gets knocked back
        while (knockbackDuration > bounce_time)
        {
            bounce_time += Time.deltaTime;
            // knockDirect is the normal vector that specifies position. It's the same as "x hat" in calc
            Vector2 knockDirect = (target.transform.position - transform.position).normalized;
            // Apply force to the slime.
            monsterBody.AddForce(-knockDirect * knockbackPower);
        }
        // Set the position of the slime to the place it just got knocked back to.
        agent.SetDestination(transform.position);
    }
    
    // Flip the sprite in relation to the position you give it.
    public void flipSprite(float PosX)
    {
        if (transform.position.x > PosX && !MobSprite.flipX)
        {
            MobSprite.flipX = true;
        }
        else if (transform.position.x < PosX && MobSprite.flipX)
        {
            MobSprite.flipX = false;
        }
    }

}
