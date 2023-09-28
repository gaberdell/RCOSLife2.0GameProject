using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour
{
    public string NPCName; //The name of the NPC
    public enum State { Idling, Walking, Running, Panicking, Dying, Following } //States that the NPC can be in
    public State currState = State.Idling;

    public Animator animate;
    public GameObject player;
    public RaycastHit hit;
    public SpriteRenderer aniSprite;

    public float HPCap; //Max Health
    public float currHP; //Current HP of the NPC

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getName() //returns the name of the NPC
    {
        return NPCName;
    }

    public string getOccupation()
    {
        return this.GetType().Name; //Get the Occupation, Assumes that the occupation of the NPC is its class
    }
}
