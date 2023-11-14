using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

/* A general class that is the basis for NPCs
 * Implements basic things that all NPCS should have.
 */

public class NPCBase : EntityBase
{
    public string NPCName; //The name of the NPC
    public enum State { Idling, Walking, Running, Panicking, Dying, Following, Sleeping } //States that the NPC can be in
    public enum State2 { Talking, Attacking, None } //States that the NPC can be in in addition to the above
    public State currState = State.Idling;
    public State2 currState2 = State2.None;
    public Hostility Hostile;
    public enum Hostility { Peaceful, Defensive, Hostile, Avoidant } //How NPC's will react to Player
    public int awareness;
    public bool Interactable; //Whether or not the Player is allowed to speak to the NPC
    public float time;
    public float timeDelay;
    //public InventorySystem Inventory;



    public SortedDictionary<string, string> dialogue = new SortedDictionary<string, string>(); //A map structure that stores a key, usually what the dialogue is for, and a dialogue


    public string getName() //returns the name of the NPC
    {
        return NPCName;
    }

    public string getOccupation()
    {
        return this.GetType().Name; //Get the Occupation, Assumes that the occupation of the NPC is its class
    }

    void Talk()
    {
        currState2 = State2.Talking;

    }
}