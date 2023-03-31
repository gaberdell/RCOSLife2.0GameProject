using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : AnimalBase
{
    // Start is called before the first frame update
    private Collider2D dc;
    private bool flying;
    private bool dead;
    private float fly_x;
    private float fly_y;
    private float fly_time;
    private GameObject self;
    int stored;
    void Start()
    {
        //status initialization
        HPCap = 45f;
        currHP = HPCap;
        currMaxSpeed = 0;
        walkspeed = 1f;
        runspeed = 1.4f;
        awareness = 5;
        currState = State.Idling;
        hungerCap = 100f;
        hunger = 100f;
        hungerDrain = 0.05f;
        hungerCap = 100f;
        //position and time initialization
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
        foodtypes.Add("Grass");
        //collider stuff
        stored = self.layer;
        dc = GetComponent<BoxCollider2D>();
        //set inital internal varibles
        dead = false;
        //collision layers
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Flying"), LayerMask.NameToLayer("Default"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Flying"), LayerMask.NameToLayer("Animal"));
    }

    // Update is called once per frame
    void Update()
    {
        //If the Duck has 0HP, it dies
        if (currHP <= 0)
        {
            currHP = 0;
            currState = State.Dying;
            dead = true;
            aniSprite.flipY = dead; //Temporary death effect. It flips upside-down
        }
        //if the flying boolean is maked,
        animate.SetBool("Flying", flying);
        if (flying){
            //slide for 1s until the duck's colider no longer intercts other things.
            if (fly_time < 0f) {
                dc.isTrigger = true;
                self.layer = stored;
                if (dc.IsTouchingLayers(LayerMask.NameToLayer("Default"))| dc.IsTouchingLayers(LayerMask.NameToLayer("Animal"))) {
                    animal.velocity = new Vector2(fly_x, fly_y);
                    self.layer = LayerMask.NameToLayer("Flying");
                } else
                {
                    dc.isTrigger = false;
                    flying = false;
                }
            } else
            {
                fly_time -= Time.deltaTime;
                animal.velocity = new Vector2(fly_x, fly_y);
            }
            }
            //This part of the Update doesn't work by frame.
            //It works on a timer defined by timeDelay
            time = time + 1f * Time.deltaTime;
            if (time >= timeDelay)
            {
                time = 0f;


                //If the duck is idling, it has a 50% chance to start wandering
                if (currState == State.Idling)
                {
                    int gen = Random.Range(0, 100);
                    if (gen > 50)
                    {
                        Walk();
                    }
                }

                //If the duck is wandering, it has a 10% chance of stopping.
                if (currState == State.Walking)
                {
                    PositionChange();
                    flipSprite();
                    int gen = Random.Range(0, 100);
                    if (gen > 90)
                    {
                        Idle();
                    }
                }
                //If the hunger is greater than or equal to  80, the duck can heal.
                if (hunger >= 80)
            {
                heal(1);
            }
                //If the hunger is less than or equal to 30, it starts looking for grass
                if (hunger <= 30)
                {
                    navi.speed = walkspeed / 2;
                    LookForFood(foodtypes);
                }
                //
                //If the hunger is 0, it starts dying
                if (hunger <= 0)
                {
                    if (hunger < 0)
                    {
                        hunger = 0;
                    }
                    takeDamage(1);
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
            //if the duck detects a non-null food, it will try to eat it. 
            if(food!=null && hunger < hungerCap - 20)
            {
                newposition = food.transform.position;
                float tempdist1 = position.sqrMagnitude;
                float tempdist2 = newposition.sqrMagnitude;
                //it will succeed in eating if the food is less than 0.05 units away from the food.
                if ((tempdist1 - tempdist2) < 0.05f){
                    animate.SetTrigger("Eating");
                    hunger += 20;
                    heal(10);
                    Destroy(food);
                    food = null;
                }
            }
            else {
                //If the duck is being pushed, it flies away. It doesn't fly as far away if the player has food. 
                if (currState == State.Pushed)
                {
                    //temp code, to be implemented.
                    newposition = transform.position;
                    position = transform.position;
                    navi.SetDestination(newposition);
                    if (!dead) {
                        self.layer = LayerMask.NameToLayer("Flying");
                        //mark the flying boolean
                        animate.SetBool("Flying", true);
                        flying = true;
                        float fly_x_temp = animal.velocity.x;
                        float fly_y_temp = animal.velocity.y;
                        fly_x = 5*fly_x_temp/Mathf.Sqrt(fly_x_temp * fly_x_temp + fly_y_temp * fly_y_temp);
                        fly_y = 5*fly_y_temp/Mathf.Sqrt(fly_x_temp * fly_x_temp + fly_y_temp * fly_y_temp);
                        fly_time = 1f;
                    }
                }
                //How the duck follows the player
                else if (currState == State.Following)
                {
                    newposition.x = player.transform.position.x;
                    newposition.y = player.transform.position.y;
                    navi.speed = walkspeed;
                    navi.SetDestination(newposition);
                    flipSprite();
                }
            }
            position = transform.position;
            }
        }
    }
