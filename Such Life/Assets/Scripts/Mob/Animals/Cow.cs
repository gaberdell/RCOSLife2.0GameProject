using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cows and sheep have similar behavior and stats
public class Cow : AnimalBase
{

    void Start()
    {
        //Initialization of the States of the Cow
        HPCap = 130;
        currHP = HPCap;
        currMaxSpeed = 0;
        walkspeed = 1f;
        runspeed = 1.4f;
        awareness = 5;
        currState = State.Idling;
        hungerCap = 100f;
        hunger = 100f;
        hungerDrain = 0.1f;

        //Useful helper variables
        position = new Vector2(transform.position.x, transform.position.y);
        newposition = position;
        time = 0f;
        timeDelay = 2f;

        //Initialization of game objects and attached components
        player = GameObject.Find("MC");
        Sprite = GetComponent<SpriteRenderer>();
        navi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navi.updateRotation = false;
        navi.updateUpAxis = false;
        food = null;
        foodtypes = new List<string>();
        foodtypes.Add("Grass");
    }

    // Update is called once per frame
    void Update()
    {
        //If the cow has 0HP, it dies
        if (currHP <= 0)
        {
            currHP = 0;
            currState = State.Dying;
            Sprite.flipY = true; //Temporary death effect. It flips upside-down
        }

        //This part of the Update doesn't work by frame.
        //It works on a timer defined by timeDelay
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0f;


            //If the Cow is idling, it has a 50% chance to start wandering
            if (currState == State.Idling)
            {
                int gen = Random.Range(0, 100);
                if (gen > 50)
                {
                    Walk();
                }
            }

            //If the Cow is wandering, it has a 10% chance of stopping.
            if (currState == State.Walking)
            {
                PositionChange();
                flipSprite();
                int gen = Random.Range(0, 100);
                if (gen > 90)
                {
                    Idle();
                }
            }

            //If the hunger is less than or equal to 30, it starts looking for grass
            if (hunger <= 30)
            {
                navi.speed = walkspeed / 2;
                LookForFood(foodtypes);
            }
            //If the hunger is 0, it starts dying
            if (hunger <= 0)
            {
                if (hunger < 0)
                {
                    hunger = 0;
                }
                takeDamage(1);
            }
            //Hunger drains if its not 0
            else
            {
                hunger = hunger - hungerCap * hungerDrain;
            }
        }

        //While the timeDelay isn't met
        else
        {
            //If the cow is being pushed, it stops resisting... will be changed 
            if (currState == State.Pushed)
            {
                newposition = transform.position;
                position = transform.position;
                navi.SetDestination(newposition);
            }
            //How the cow follows the player
            else if (currState == State.Following)
            {
                newposition.x = player.transform.position.x;
                newposition.y = player.transform.position.y;
                navi.speed = walkspeed;
                navi.SetDestination(newposition);
                flipSprite();
            }
        }
        position = transform.position;

    }



    void FixedUpdate()
    {

    }
}
