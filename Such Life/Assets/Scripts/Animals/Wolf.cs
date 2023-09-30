using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : AnimalBase
{
    private Collider2D dc;
    private bool dead;
    private GameObject self;
    int stored;
    // Start is called before the first frame update
    void Start()
    {
        HPCap = 70f;
        currHP = HPCap;
        currMaxSpeed = 1;
        walkspeed = 1;
        runspeed = 2;
        awareness = 25;
        currState = State.Walking;
        hungerCap = 100f;
        hunger = 100f;
        hungerDrain = 0.05f;
        
        position = new Vector2(transform.position.x, transform.position.y);
        newposition = position;
        time = 0f;
        timeDelay = 1f;
        //object initialization
        player = GameObject.Find("MC");
        animate = GetComponent<Animator>();
        aniSprite = GetComponent<SpriteRenderer>();
        navi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        self = navi.gameObject;
        navi.updateRotation = false;
        navi.updateUpAxis = false;
        food = null;
        foodtypes = new List<string>();
        foodtypes.Add("CanFly");
        //collider stuff
        stored = self.layer;
        dc = GetComponent<BoxCollider2D>();
        //set inital internal varibles
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currHP <= 0) {
            currHP = 0;
            currState = State.Dying;
            dead = true;
            aniSprite.flipY = dead;
        }
        if (currHP < HPCap) {
            currState = State.Following;
        }
        if (currState == State.Walking) {
            LookForFood(foodtypes);
        }
    }
}
