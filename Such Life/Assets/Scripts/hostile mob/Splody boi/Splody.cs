using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Splody : mobBase
{
    [SerializeField]
    public int explosionRange;
    // Start is called before the first frame update
    [SerializeField] double explodeTimer;
    [SerializeField] bool exploded;
    [SerializeField] float speed;
    void Start()
    {
        monsterBody = GetComponent<Rigidbody2D>();
        
        target = GameObject.Find("MC").transform;
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
        explosionRange = 3;
        explodeTimer = 5f;
        exploded = false;
        speed = 1f;
}

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("MC").transform;
        time += 1f * Time.deltaTime;
        distance = Vector2.Distance(target.position, currPosition);
        StateChange();

        monsterBody.MovePosition(currPosition);
        agent.isStopped = false;
        agent.acceleration = 200;
        if (currState == State.Chasing)
        {
            chasing(distance);
        }
        else if (currState == State.Wander)
        {   
            if (time >= time_move){
                wander();
            }
        }
        else if (currState == State.Attacking)
        {
            chargeExplosion();
        }
        
        //If and only if the agent active and is on a NavMesh, it should set the agent's destination.
        //
        if (!exploded) { 
        agent.SetDestination(currPosition);
        }
    }

    void wander()
    {
        //float oldPosX = currPosition.x;
        PositionChange();
        //flipSprite(oldPosX);
    }

    void chasing(float distance)
    {
        speed *= Time.deltaTime;
        //angle = Mathf.Atan2((target.position.y - currPosition.y) , (target.position.x - currPosition.x));
        //Vector2 conversion = currPosition;
        //agent.SetDestination(conversion);
        currPosition = Vector2.MoveTowards(currPosition,target.position, speed);
        print("Debug: speed is" + speed);
        //flipSprite(oldPosX);
        speed /= Time.deltaTime;
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
        agent.isStopped = true;
        if (collision.gameObject.tag == "Player" && currState == State.Attacking)
        {
            agent.isStopped = false;
            //bounce();
        }
    }
    void chargeExplosion()
    {
        if(explodeTimer <= 0) {
            explode();
        }
        else {  
            explodeTimer -= Time.deltaTime; 
        }
    }
    void StateChange()
    {
        //
        if ((distance < (explosionRange+0.3) && currState == State.Attacking) || distance <= explosionRange * 1) {
            currState = State.Attacking;
        }
        else if (distance <= alertRange && !playerSighted)
        {
            playerSighted = true;
            currState = State.Chasing;
           // flipSprite(-target.position.x);
        }
        else if (distance >= (explosionRange + 0.3) && currState == State.Attacking)
        {
            currState = State.Chasing;
            explodeTimer = 5f;
        }
        else if(distance >= alertRange)
        {
            playerSighted = false;
            currState = State.Wander;
        }
    }
    void explode()
    {
        //to be implemented; if any mob or the player is in the explosion range, it takes damage
        //Destroy the object without dropping anything.
        Destroy(gameObject);
        //to be implemented, add an explosion sprite
        //set the exploded value to true
        exploded = true;
    }
    /*void bounce()
    {
        angle = Mathf.Atan2((target.position.y - currPosition.y), (target.position.x - currPosition.x));
        angle *= -1;
        agent.acceleration = 10;
        currPosition = new Vector2(currPosition.x + 2 * Mathf.Cos(angle), currPosition.y + 3 * Mathf.Sin(angle));
    } */

  /*  void flipSprite(float PosX)
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
  */
}
