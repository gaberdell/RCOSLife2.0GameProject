using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* A general class that is the basis for NPCs 
 * Implements basic things that all NPCS should have.
 */

public class NPCBase : MonoBehaviour
{
    public string NPCName; //The name of the NPC
    public enum State { Idling, Walking, Running, Panicking, Dying, Following, Talking, Attacking } //States that the NPC can be in
    public State currState = State.Idling;
    public enum Hostility { Peaceful, Defensive, Hostile, Avoidant }

    public Animator animate;
    public GameObject player;
    public RaycastHit hit;
    public SpriteRenderer NPCSprite;

    public float HPCap; //Max Health
    public float currHP; //Current HP of the NPC

    public SortedDictionary<string,string> dialogue = new SortedDictionary<string,string>(); //A map structure that stores a key, usually what the dialogue is for, and a dialogue

    public NavMeshAgent navi;
    public Vector2 position; //The current position of the NPC in a Vector2 object
    public Vector2 newposition; //Where the NPC wants to go

    public string getName() //returns the name of the NPC
    {
        return NPCName;
    }

    public string getOccupation()
    {
        return this.GetType().Name; //Get the Occupation, Assumes that the occupation of the NPC is its class
    }

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
        else if (gen == 2)
        {
            newposition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
        }
        navi.speed = currSpeed * 2;
        navi.SetDestination(newposition);
    }
}
