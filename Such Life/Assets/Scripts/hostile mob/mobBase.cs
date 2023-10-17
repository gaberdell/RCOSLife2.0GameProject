using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobBase : EntityBase
{

    //Some common feature for enemies
    
    public float angle;
    public float alertRange; //min distance from target required for mob to chase.
    public float knockbackDuration;
    public float knockbackPower;
    

    public bool playerSighted = false; //check whether player is in monster's sight
    public Vector2 currPosition; //current position
    public float distance; 
    public float time; //unique time variable
    public float time_move; //time until Update() is called.

    //States for Enemies
    public enum State { Idling, Wander, Chasing, Attacking };
    public State currState;

    //public Animator an;
    public GameObject monsterObj;
    public GameObject player;
    // public Vector2 newPosition;

    public Transform target;
    public Rigidbody2D monsterBody;

    public string GetMob() //Returns the Mob Type
    {
        return this.GetType().Name;
    }

    public void Idle()
    {
        currState = State.Idling;
    }

    public void Wander()
    {
        currState = State.Wander;
    }

    public void Chasing()
    {
        currState = State.Chasing;
    }
    public LayerMask targetLayerMobs;  


    void Start()
    {
        currSpeed = 5.0f;
    }
    /*void PositionChange()
    {

        //the character will walk in random angles for every 1.3s
        float angle = Random.Range(0f, 360f);
        Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward); //rotate the character with random angle
        Vector3 latestUP = rotate* Vector3.up;
        latestUP.z = 0;
        latestUP.Normalize();
        transform.up = latestUP;

        timeToChangeDirection -= Time.deltaTime;

        monster.velocity = transform.up*2;
    }

    void Walk()
    {

    }*/
}
