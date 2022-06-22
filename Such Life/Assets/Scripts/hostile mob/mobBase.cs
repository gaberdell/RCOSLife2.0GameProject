using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobBase : MonoBehaviour
{

    //Some common feature for enemies
    public float walkSpeed;
    public int damage; //damage that a enemy make in fighting
    public int maxHealth; //the total health of an enemy
    public int currHealth; //current health
    public Rigidbody2D monster;
    public Vector2 currPosition; //current position 
    public float timeToChangeDirection = 1.3f;
    //public Vector3 newUP;

    void Start()
    {
        PositionChange();
    }

    // Update is called once per frame
    void Update()
    {
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            PositionChange();
        }

        monster.velocity = transform.up*2;
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
        timeToChangeDirection = 1.3f;
    }

    void Walk()
    {

    }
}
