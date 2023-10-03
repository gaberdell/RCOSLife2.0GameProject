using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*  A class that includes a movement function. So many things need this.
 * 
 */

public abstract class EntityMovement : MonoBehaviour
{

    public float currMaxSpeed; //Current possible max speed
    public float currSpeed; //Current Speed of Animal
    public Vector2 position; //The current position of the animal in a Vector2 object
    public Vector2 newposition; //The position that the animal wants to reach
    public NavMeshAgent navi; //Hey, Listen!

    //random pos
    public virtual void PositionChange()
    {
        currSpeed = Random.Range(0, currMaxSpeed);
        float posxmin = transform.position.x - currSpeed;
        float posxmax = transform.position.x + currSpeed;
        float posymin = transform.position.y - currSpeed;
        float posymax = transform.position.y + currSpeed;

        int gen = Random.Range(0, 2);
        if (gen == 0)
        {
            newposition = new Vector2(Random.Range(posxmin, posxmax), transform.position.y);
        }
        else if (gen == 1)
        {
            newposition = new Vector2(transform.position.x, Random.Range(posymin, posymax));
        }
        else if (gen == 2)
        {
            newposition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
        }
        navi.speed = currSpeed * 2;
        navi.SetDestination(newposition);
    }

}

