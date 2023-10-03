using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* A general class that is the basis for NPCs 
 * Implements basic things that all NPCS should have.
 */

public class NPCBase : EntityMovement
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


    public string getName() //returns the name of the NPC
    {
        return NPCName;
    }

    public string getOccupation()
    {
        return this.GetType().Name; //Get the Occupation, Assumes that the occupation of the NPC is its class
    }

    
}
