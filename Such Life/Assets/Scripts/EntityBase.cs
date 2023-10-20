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
    public Vector2 position; //The current position of the entity in a Vector2 object
    public Vector2 newposition; //The position that the animal wants to reach
    public NavMeshAgent navi; //Hey, Listen!
    public SpriteRenderer Sprite;
    public playerAction playerControl; //Allows player to interact with entity
    public int maxHealth; //the total health of an entity
    public int currHealth; //current health
    public float speed; //the speed
    public int damage; //damage that a enemy make in fighting
    public float critChance; //Crit chance for entity when they attack. Example: 15% is 0.15f
    public float critDamage; //Crit damage multiplier that is multiplied to attack
    public int defense; //The defense stat of an entity
    public string variation; //The string that saves what type the mob is


    public GameObject player;
    public Animator animate;
    public RaycastHit hit;


    //random pos
    public virtual void PositionChange()
    {
        speed = Random.Range(0, currMaxSpeed);
        float posxmin = transform.position.x - speed;
        float posxmax = transform.position.x + speed;
        float posymin = transform.position.y - speed;
        float posymax = transform.position.y + speed;

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
        navi.speed = speed * 2;
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
    //This function returns the value of the current amount of HP the Animal has
    public float getHealth()
    {
        return currHealth;
    }

    public void takeDamage(int dmg)
    {
        currHealth -= dmg;
    }

    public void heal(int val)
    {
        currHealth += val;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }

    public float getDistance(GameObject thing)
    {
        return ((Vector2)thing.transform.position - position).sqrMagnitude;
    }

    public void moveTo(Vector2 pos)
    {
        newposition = pos;
        navi.SetDestination(newposition);
        flipSprite();
    }

    public Texture2D getSpriteVariant(string directory) // This function will be used to pick a random sprite for an entity within a folder
    {

        return null;
    }

    public Texture2D getSpecificSprite(string directory, string variation) //This function will get a specific sprite
    {
        string fullDir = directory + '/' + variation + ".png";
        var rawData = System.IO.File.ReadAllBytes(fullDir);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(rawData);
        return tex;
    }
}

