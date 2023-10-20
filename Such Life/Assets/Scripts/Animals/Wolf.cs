using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : AnimalBase
{
    public Collider2D dc;
    private bool dead;
    private GameObject self;
    int stored;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currHealth = maxHealth;
        currMaxSpeed = 1;
        walkspeed = 1;
        runspeed = 2;
        awareness = 25;
        currState = State.Idling;
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
        Sprite = GetComponent<SpriteRenderer>();
        navi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        self = navi.gameObject;
        // self.Class = this;
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
        if (currHealth <= 0) {
            currHealth = 0;
            currState = State.Dying;
            dead = true;
            Sprite.flipY = dead;
        }

        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay) {
            time = 0f;

            //If the wolf is idling, it has a 30% chance to start wandering
            if (currState == State.Idling) {
                int gen = Random.Range(0, 100);
                if (gen < 30) {
                    Walk();
                }
            }

            //If the wolf is wandering, it has a 10% chance of stopping.
            if (currState == State.Walking) {
                PositionChange();
                int gen = Random.Range(0, 100);
                if (gen < 10) {
                    Idle();
                }
            }
            //If the hunger is greater than or equal to 80, the wolf can heal.
            if (hunger >= 80)
            {
                heal(1);
            }
            //If the hunger is less than or equal to 30, it starts looking for food
            if (hunger <= 30)
            {
                navi.speed = runspeed;
                currState = State.Running;
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
                // print(hunger);
            }
        }
        //While the timeDelay isn't met
        else {
            if (currState == State.Following)
            {
                navi.speed = runspeed;
                moveTo(player.transform.position);
            }
            position = transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        // Try to eat non-null food and stop moving
        if (collision.gameObject == food) {
            currState = State.Eating;
            hunger += 50;
            heal(20);
            Destroy(food);
            food = null;
            moveTo(transform.position);
            currState = State.Idling;
        }
    }
}
