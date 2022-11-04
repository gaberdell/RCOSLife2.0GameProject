using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : AnimalBase
{

    // Start is called before the first frame update
    void Start()
    {
        //Initialization of the States of the Sheep
        HPCap = 90;
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
        aniSprite = GetComponent<SpriteRenderer>();
        navi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navi.updateRotation = false;
        navi.updateUpAxis = false;   
        food = null;
    }

    // Update is called once per frame
    void Update()
    {
        //If the sheep has 0HP, it dies
        if(currHP <= 0)
        {
            currHP = 0;
            currState = State.Dying;
            aniSprite.flipY = true; //Temporary death effect. It flips upside-down
        }

        //This part of the Update doesn't work by frame.
        //It works on a timer defined by timeDelay
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0f;

            //If the Sheep is Hungry, it looks for the closest grass in its detection range
            if (currState == State.Hungry)
            {   
                food = findClosestObj("Grass");
                if (food)
                {
                    newposition = food.transform.position;
                    navi.SetDestination(newposition);
                    float tempdist1 = Mathf.Round(position.sqrMagnitude * 10);
                    float tempdist2 = Mathf.Round(newposition.sqrMagnitude * 10);
                    if (tempdist1 == tempdist2)
                    {
                        hunger += 20;
                        currHP += 10; //Eating food recovers 20 hunger and 10hp. WILL CHANGE
                        if(currHP > HPCap)
                        {
                            currHP = HPCap;
                        }
                        Destroy(food);
                        food = null;
                    }
                }
            }

            //If the sheep is idling, it has a 50% chance to start wandering
            if (currState == State.Idling)
            {
                int gen = Random.Range(0, 100);
                if (gen > 50)
                {
                    Walk();
                }
            }

            //If the sheep is wandering, it has a 30% chance of stopping.
            if (currState == State.Walking)
            {
                PositionChange();
                int gen = Random.Range(0, 100);
                if (gen > 70)
                {
                    Idle();
                }
            }
            
            //If the mob is following something, it has a 10% chance to stop following
            if(currState == State.Following)
            {
                int gen = Random.Range(0, 100);
                if (gen > 90)
                {
                    Idle();
                }
            }

            //If the hunger is less than or equal to 30, it starts looking for grass
            if(hunger <= 30)
            {
                touchGrass();
            }
            //If the hunger is 0, it starts dying
            if(hunger <= 0)
            {
                if(hunger < 0)
                {
                    hunger = 0;
                }
                currHP = currHP - 3;
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
            //If the sheep is being pushed, it stops resisting... will be changed 
            if (currState == State.Pushed)
            {
                newposition = transform.position;
                position = transform.position;
                navi.SetDestination(newposition);
                int gen = Random.Range(0, 100);
                if(gen > 90)
                {
                    Follow();
                }
            }
            //How the sheep follows the player
            else if(currState == State.Following)
            {
                newposition.x = player.transform.position.x;
                newposition.y = player.transform.position.y;
                navi.speed = walkspeed*2;
                navi.SetDestination(newposition);
            }
        }
        position = transform.position;
        flipSprite();
    }

    void touchGrass() //When the hunger is 0, this will trigger
    {
        currState = State.Hungry;

    }

    void FixedUpdate()
    {

    }

}
   