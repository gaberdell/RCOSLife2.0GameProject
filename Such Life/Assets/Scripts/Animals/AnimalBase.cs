using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * This is the base for all animal AI
 * This class does not implement AI for any specific animal
*/
public class AnimalBase : MonoBehaviour
{   
    //The State and Stats of animal
    public enum State { Idling, Walking, Running, Eating, Panicking, Dying, Following, Hungry, Pushed } //The different states the animal can be in
    public State currState = State.Idling; 
    public int awareness; //When the animal can detect objects. Is different for different animals, and can change depending on state
    public float walkspeed; //How fast the animal walks
    public float runspeed; //How fast the animal runs
    public Rigidbody2D animal;
    public float HPCap; //Max Health
    public float currHP; //Current HP of Animal
    public float currMaxSpeed; //Current possible max speed
    public float currSpeed; //Current Speed of Animal
    public float weight; //Determines how much the animal will get pushed
    public float hungerCap; //Max Hunger Value
    public float hunger; //Hunger of the animal
    public float hungerDrain; //How fast the hunger of the animal drains, by percentage per second

    public Animator animate;
    public Vector2 position;
    public Vector2 newposition;
    public float time;
    public float timeDelay;
    public bool reached; //Determines if the animal has reached its destination
    public GameObject player;
    public RaycastHit hit;
    public SpriteRenderer aniSprite;
    public GameObject food;


    public NavMeshAgent navi; //Hey, Listen!


    //random pos
    public void PositionChange()
    {
        currSpeed = Random.Range(0, currMaxSpeed);
        float posxmin = transform.position.x - currSpeed;
        float posxmax = transform.position.x + currSpeed;
        float posymin = transform.position.y - currSpeed;
        float posymax = transform.position.y + currSpeed;

        int gen = Random.Range(0, 2);
        if (gen == 0) 
        {
            newposition = new Vector2(Random.Range(posxmin, posxmax), transform.position.y);
        }
        else if (gen == 1)
        {
            newposition = new Vector2(transform.position.x, Random.Range(posymin, posymax));
        }
        else if(gen == 2)
        {
            newposition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
        }
        navi.speed = currSpeed*2;
        navi.SetDestination(newposition);
    }

    public string  getAnimal()
    {
        return this.GetType().Name; //Get the animal type
    }

    public void Run()
    {
        currState = State.Running;
        currMaxSpeed = runspeed;
    }
    
    public void Walk()
    {
        currMaxSpeed = walkspeed;
        currState = State.Walking;
    }

    public void Idle()
    {
        currMaxSpeed = 0;
        currState = State.Idling;
    }

    public void Hungry()
    {
        currState = State.Hungry;
    }

    public void Follow()
    {
        currState = State.Following;
    }

    public float getHP()
    {
        return currHP;
    }

    public float getSpeed()
    {
        return currSpeed;
    }

   public void flipSprite()
    {
        Vector2 direction = newposition - position; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //flip sprites based on the direction of the target and "this"
        if (angle >= 90 || angle <= -90)
        {
            //face left
            aniSprite.flipX = false;

        }
        else
        {
            aniSprite.flipX = true;
        }
    }

    public GameObject findClosestObj(string tag)
    {
        GameObject[] things;
        try
        {
            things = GameObject.FindGameObjectsWithTag(tag);
            GameObject closest = null;
            float distance = Mathf.Infinity;
            foreach(GameObject thing in things)
            {
                Vector2 diff = (Vector2)thing.transform.position - position;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    closest = thing;
                    distance = curDistance;
                }
               }
            if (distance <= awareness)
            {
                return closest;
            }
            else
            {
                return null;
            }
        }
            
             catch
        {
            return null;
        }
    }
       

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
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
}
