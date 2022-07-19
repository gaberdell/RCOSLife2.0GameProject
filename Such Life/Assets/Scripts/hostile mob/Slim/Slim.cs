using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slim : mobBase
{
    public float awareness;
    public float radius;
    public float angle;
    public SpriteRenderer slimeSprite;
    public float wanderingSpeed;
    //public UnityEngine.AI.NavMeshAgent player;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 5;
        damage = 2;
        currHealth = maxHealth;
        walkSpeed = 1f;
        wanderingSpeed = 0.01f;
        currPosition = new Vector2(transform.position.x, transform.position.y);
        playerSighted = false;
    }

    void FixedUpdate(){

        timeToChangeDirection -= Time.deltaTime;

        /*if (timeToChangeDirection <= 0)
        {
            PositionChange();
            flipSprite(monsterObj);
        }*/

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, .00f);

        //an.SetBool("playerSighted", false);
        if (timeToChangeDirection <= 2f)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, newPosition, wanderingSpeed);
        }

        if (timeToChangeDirection <= 0f)
        {
            PositionChange();
            timeToChangeDirection = 2.8f;
        }

        //monster.velocity = transform.up/2;
    }

    void PositionChange()
    {

        posxmin = transform.position.x - 5.0f;
        posxmax = transform.position.x + 5.0f;
        posymin = transform.position.y - 5.0f;
        posymax = transform.position.y + 5.0f;


        newPosition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
        
    }

    void chasing(){

    }

    void wandering(){
        //the character will walk in random angles for every 1.3s
        angle = Random.Range(0f, 360f);
        Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward); //rotate the character with random angle
        Vector3 latestUP = rotate* Vector3.up;
        latestUP.z = 0;
        latestUP.Normalize();
        transform.up = latestUP;
        timeToChangeDirection = 2.0f;


    }

    //Checking on the FOV circle in an interval
    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            //FOV();
        }
    }

    private void flipSprite(GameObject target)
    {
        Vector3 direction = target.transform.position - this.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //flip sprites based on the direction of the target and "this"
        if (angle >= 90 || angle <= -90)
        {
            //face left
            slimeSprite.flipX = false;
            
        }
        else
        {
            slimeSprite.flipX = true;
        }
    }

    //check for collider on our target layer
    /*private void FOV()
    {
        Collider2D[] rangeCheckPlayer = Physics2D.OverlapCircleAll(transform.position, radius, targetLayerPlayer);
        //Check to see if anything in the collison box
        if (rangeCheckPlayer.Length > 0)
        {
            //check to see if the player is detected within range
            Transform targetPlayer = rangeCheckPlayer[0].transform;
            Vector2 directionToTargetPlayer = (targetPlayer.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTargetPlayer) < angle / 2)
            {
                float distanceToTargetPlayer = Vector2.Distance(transform.position, targetPlayer.position);
                //check to see if the Raycast hit the obstructionLayer object or not, if not
                //the enemy spotted the player
                if (!Physics2D.Raycast(transform.position, directionToTargetPlayer, distanceToTargetPlayer, obstructionLayer))
                {
                    playerSighted = true;
                }
                else
                {
                    playerSighted = false;
                }
            }
            else
            {
                playerSighted = false;
            }
        }
        else if (playerSighted)
        {
            playerSighted = false;
        }
    }*/

}
