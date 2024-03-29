using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*  A class that includes a movement function. So many things need this.
 *  As a virtual method, it can be overrided anytime
 */

public abstract class EntityBase : MonoBehaviour
{

    public float currMaxSpeed; //Current possible max speed
    public float currSpeed; //Current Speed of Animal
    public Vector2 position; //The current position of the entity in a Vector2 object
    public Vector2 newposition; //The position that the animal wants to reach
    public NavMeshAgent navi; //Hey, Listen!
    public SpriteRenderer Sprite;
    public playerAction playerControl; //Allows player to interact with entity

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

    //This function flips the sprite of the entity
    //Used to make sure the entity faces the correct direction. Will be removed in the future when animations are implemented
    public void flipSprite()
    {
        Vector2 direction = newposition - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //flip sprites based on the direction of the target and "this"
        if (angle >= 90 || angle <= -90)
        {
            //face left
            Sprite.flipX = false;

        }
        else
        {
            Sprite.flipX = true;
        }
    }

    //This function finds the closest Object with the tag given
    public GameObject findClosestObj(string tag, float radius)
    {
        GameObject[] things;
        //When no object with the tag is found, Unity returns an Error
        try
        {
            //Get all the objects with the given tag in the scene
            things = GameObject.FindGameObjectsWithTag(tag);
            GameObject closest = null;
            float distance = Mathf.Infinity;

            //Loop through the list and compare their distances to the Animal
            foreach (GameObject thing in things)
            {
                Vector2 diff = (Vector2)thing.transform.position - position;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    closest = thing;
                    distance = curDistance;
                }
            }
            if (distance <= radius)
            {
                return closest;
            }
            else
            {
                return null;
            }
        }
        //If an error is returned, return NULL
        catch
        {
            return null;
        }
    }
}

