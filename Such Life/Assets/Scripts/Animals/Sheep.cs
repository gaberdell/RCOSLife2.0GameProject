using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : AnimalBase
{

    // Start is called before the first frame update
    void Start()
    {
        HPCap = 80;
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
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0f;
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
        }
        else
        {
            if (currState == State.Pushed)
            {
                newposition = transform.position;
                position = transform.position;
            }
            else
            {
                transform.position = Vector2.MoveTowards(position, newposition, walkspeed / 300);
                position = transform.position;
            }
        }
    }

    void FixedUpdate()
    {

    }
}
