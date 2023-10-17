using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * This is the base for all animal AI
 * This class does not implement AI for any specific animal
*/
public class AnimalBase : EntityBase
{   
    //The State and Stats of animal
    public enum State { Idling, Walking, Running, Eating, Panicking, Dying, Following, Pushed } //The different states the animal can be in
    public State currState = State.Idling; 
    public int awareness; //When the animal can detect objects. Is different for different animals, and can change depending on state
    public float walkspeed; //How fast the animal walks
    public float runspeed; //How fast the animal runs
    public Rigidbody2D animal;
    public float HPCap; //Max Health
    public float currHP; //Current HP of Animal

    public float weight; //Determines how much the animal will get pushed
    public float hungerCap; //Max Hunger Value
    public float hunger; //Hunger of the animal
    public float hungerDrain; //How fast the hunger of the animal drains, by percentage per second
    public int size; //Depending on the size, there are predetermined stats

    public Animator animate;


    public float time;
    public float timeDelay;
    public bool reached; //Determines if the animal has reached its destination
    public GameObject player;
    public RaycastHit hit;
    public GameObject food; //The variable that references the food object that the animal will go after
    public List<string> foodtypes; //What this animal will eat
    public List<string> drops; //What the animal will drop when it dies
    
    public string  getAnimal()
    {
        return this.GetType().Name; //Get the animal type
    }


    //The following functions change the state of the Animal:

    //Sets the Animal to Running State.
    //It also sets the max speed of the animal to their running speed
    public void Run()
    {
        currState = State.Running;
        currMaxSpeed = runspeed;
    }
    
    //Sets the Animal to Walking State.
    //It also sets the max speed of the animal to their walking speed
    public void Walk()
    {
        currMaxSpeed = walkspeed;
        currState = State.Walking;
    }


    //Sets the state of the Animal to Idling
    //They don't move in this state so the speed is 0
    public void Idle()
    {
        currMaxSpeed = 0;
        currState = State.Idling;
    }

    //Sets the state of the Animal to Following
    //In this state, the animal follows the player
    public void Follow()
    {
        currState = State.Following;
    }

    //This function returns the value of the current amount of HP the Animal has
    public float getHP()
    {
        return currHP;
    }

    //This function returns the speed the animal is currently moving at
    public float getSpeed()
    {
        return currSpeed;
    }



    //A simple function that makes the animal take the specified amount of damage
    public void takeDamage(int val)
    {
        currHP -= val;
    }

    public void heal(int val)
    {
        currHP += val;
        if(currHP > HPCap)
        {
            currHP = HPCap;
        }
    }


       
    void OnCollisionEnter2D(Collision2D collision)
    {
        //If it is the player, it gets pushed. Will be changed to other entities in the future
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "MC")
        { 
                currState = State.Pushed;            
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (currState == State.Pushed)
        {
            newposition = transform.position;
            currState = State.Idling;
        }
    }

    public float getDistance(GameObject thing)
    {
        return ((Vector2)thing.transform.position - position).sqrMagnitude;
    }

    
    //Looks for closest food that the animal can eat
    public void LookForFood(List<string> foods) 
    {
        float currentclosest = -1f;
        foreach(var thing in foods) {
            food = findClosestObj(thing, awareness);

            
            if (food)
            {
                if (currentclosest == -1f)
                {
                    currentclosest = getDistance(food);
                }

                if (getDistance(food) < currentclosest)
                {
                    currentclosest = getDistance(food);
                    newposition = food.transform.position;
                    navi.SetDestination(newposition);
                    
                }
                else
                {
                    return;
                }
                
            }
        }
        

    }
}
