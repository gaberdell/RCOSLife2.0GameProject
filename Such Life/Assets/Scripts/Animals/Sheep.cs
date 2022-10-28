using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : AnimalBase
{

    // Start is called before the first frame update
    void Start()
    {
        HPCap = 90;
        currHP = HPCap;
        currMaxSpeed = 0;
        walkspeed = 1f;
        runspeed = 1.4f;
        awareness = 5;
        currState = State.Idling;
        position = new Vector2(transform.position.x, transform.position.y);
        newposition = position;
        time = 0f;
        timeDelay = 2f;
        player = GameObject.Find("MC");
        aniSprite = GetComponent<SpriteRenderer>();
        navi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navi.updateRotation = false;
        navi.updateUpAxis = false;
        hungerCap = 100f;
        hunger = 100f;
        hungerDrain = 0.1f;
        food = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(currHP <= 0)
        {
            currHP = 0;
            currState = State.Dying;
            aniSprite.flipY = true;
        }
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0f;
            if (currState == State.Hungry)
            {
                food = findClosestObj("Grass");
                newposition = food.transform.position;
                navi.SetDestination(newposition);
                float tempdist1 = Mathf.Round(position.sqrMagnitude * 100);
                float tempdist2 = Mathf.Round(newposition.sqrMagnitude * 100);
                if(tempdist1 == tempdist2)
                {
                    Destroy(food);
                    food = null;
                }
            }

            if (currState == State.Idling)
            {
                int gen = Random.Range(0, 100);
                if (gen > 50)
                {
                    Walk();
                }
            }

            if (currState == State.Walking)
            {
                PositionChange();
                int gen = Random.Range(0, 100);
                if (gen > 70)
                {
                    Idle();
                }
            }
            
            if(currState == State.Following)
            {
                int gen = Random.Range(0, 100);
                if (gen > 90)
                {
                    Idle();
                }
            }
            if(hunger <= 30)
            {
                touchGrass();
            }
            if(hunger <= 0)
            {
                if(hunger < 0)
                {
                    hunger = 0;
                }
                currHP = currHP - 3;
            }
            else
            {
                hunger = hunger - hungerCap * hungerDrain;
            }
        }
        else
        {
            if (currState == State.Pushed)
            {
                newposition = transform.position;
                position = transform.position;
                navi.SetDestination(newposition);
                int gen = Random.Range(0, 100);
                if(gen > 90)
                {
                    Follow();
                }
            }

            else if(currState == State.Following)
            {
                newposition.x = player.transform.position.x;
                newposition.y = player.transform.position.y;
                navi.speed = walkspeed*2;
                navi.SetDestination(newposition);
            }
        }
        position = transform.position;
        flipSprite();
    }

    void touchGrass() //When the hunger is 0, this will trigger
    {
        currState = State.Hungry;

    }

    void FixedUpdate()
    {

    }

}
   