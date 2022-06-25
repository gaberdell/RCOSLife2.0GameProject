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

            transform.position = Vector2.MoveTowards(position, newposition, walkspeed / 300);
            position = transform.position;
        }
    }

        void OnCollisionEnter2D(Collision2D collision)
        {

        if (collision.gameObject.tag == "Player")
        {
            float posdiffx = transform.position.x - player.transform.position.x * 4;
            float posdiffy = transform.position.x - player.transform.position.x * 4;
            newposition.x = transform.position.x - posdiffx;
            newposition.y = transform.position.y - posdiffy;
        }
    }
}
