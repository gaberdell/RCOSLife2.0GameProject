using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobBase : MonoBehaviour
{

    //Some common feature for enemies
    public float walkSpeed;
    public int damage; //damage that a enemy make in fighting
    public int maxHealth; //the total health of an enemy
    public int currHealth; //current health
    public Rigidbody2D monster;
    public bool playerSighted = false; //check whether player is in monster's sight
    public Vector2 currPosition; //current position 
    public Vector2 initialPosition; //initial position
    public float timeToChangeDirection = 1.3f;
    public float time_move;

    //States for Enemies
    public enum State { Idling, Wander, Chasing, Dying };
    public State currState;


    //public Animator an;
    public GameObject monsterObj;
    public GameObject player;
    public Vector2 newPosition;

    public float posxmin;
    public float posymin;
    public float posxmax;
    public float posymax;

    public float awareness;
    public float radius;
    public float angle;
    public SpriteRenderer MobSprite;
    public float wanderingSpeed;
    public Transform target;
    public UnityEngine.AI.NavMeshAgent agent;

    public void PositionChange()
    {
        posxmin = transform.position.x - 5.0f;
        posxmax = transform.position.x + 5.0f;
        posymin = transform.position.y - 5.0f;
        posymax = transform.position.y + 5.0f;

        newPosition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
    }

    public string GetMob() //Returns the Mob Type
    {
        return this.GetType().Name;
    }

    public void Idle()
    {
        currstate = State.Idling;
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
