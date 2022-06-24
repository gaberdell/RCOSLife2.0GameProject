using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slim : mobBase
{
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 5;
        damage = 2;
        currHealth = maxHealth;
        walkSpeed = 1f;
        currPosition = new Vector2(transform.position.x, transform.position.y);
        playerSighted = false;
    }

    void Update(){

        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            PositionChange();
        }

        monster.velocity = transform.up/2;
    }

    void PositionChange()
    {

        //the character will walk in random angles for every 1.3s
        float angle = Random.Range(0f, 360f);
        Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward); //rotate the character with random angle
        Vector3 latestUP = rotate* Vector3.up;
        latestUP.z = 0;
        latestUP.Normalize();
        transform.up = latestUP;
        timeToChangeDirection = 2.0f;

    }



    /*public void PositionChange()
    {
        float posxmin = transform.position.x - 1.0f;
        float posxmax = transform.position.x + 1.0f;
        float posymin = transform.position.y - 1.0f;
        float posymax = transform.position.y + 1.0f;

        currPosition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        time_move -= Time.deltaTime;
        if(playerSighted){ // chase state
            playerObj = GameObject.Find("player");
            transform.position = Vector2.MoveTowards(this.transform.position, playerObj.transform.position, walkSpeed * Time.deltaTime);
        } else { //wandering state
            //wandering around with breaks
            transform.position = Vector2.MoveTowards(this.transform.position, monster.transform.position, .00f);
    
            if (time_move <= 2f)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, currPosition, walkSpeed/400);
            }

            if (time_move <= 0f)
            {
                PositionChange();
                time_move = 2.8f;
            }
        }
    }*/



}
