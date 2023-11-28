using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BaseSkeleton : mobBase
{
    public Animator spanim;
    [SerializeField]
    public float prevX;
    public float prevY;
    public float invincibilityDuration = 3.0f;
    protected bool isInvincible = false;

    protected float timeToRecover = 0;

    protected Animator skeletonAnimator;

    public float proximityDetectionDistance = 5.0f;
    protected bool playerInRange = false;

    public GameObject bonePrefab; // Prefab for the bone projectile
    public Transform boneSpawnPoint; // Transform where bones are spawned
    public float boneAttackCooldown = 2.0f;
    protected float lastBoneAttackTime = 0;

    public GameObject fingerPrefab; // Prefab for the finger projectile
    public Transform fingerSpawnPoint; // Transform where fingers are spawned
    public float fingerAttackCooldown = 4.0f;
    protected float lastFingerAttackTime = 0;
    public float boneSpeed = 10.0f;



    protected virtual void Awake()
    {
        skeletonAnimator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {        currHealth = 100;
        SetCrumpledState(true); // Initially in crumpled state
         monsterBody = GetComponent<Rigidbody2D>();
        monsterBody.drag = 10f;
        
        target = GameObject.Find("MC").transform;
        navi = GetComponent<NavMeshAgent>();
        navi.acceleration = 200;
        navi.stoppingDistance = 1f;
        navi.autoBraking = false;
        navi.updateRotation = false;
        navi.updateUpAxis = false;
        spanim = GetComponent<Animator>();
        currState = State.Wander;
        currPosition = new Vector2(transform.position.x, transform.position.y);
        prevX = 0;
        prevY = 0;
        alertRange = 50f;
    }

    protected virtual void Update()
    {
        prevX = currPosition.x;
        prevY = currPosition.y;
        target = GameObject.Find("MC").transform;
        time += 1f * Time.deltaTime;
        distance = Vector2.Distance(target.position, currPosition);
        StateChange();

        //monsterBody.MovePosition(currPosition);
        navi.isStopped = false;
        navi.acceleration = 200;
        if (currState == State.Chasing)
        {
            spanim.SetBool("Attacking", false);
            chasing(distance);
            if (target.position.x - currPosition.x > 2f && Mathf.Abs(currPosition.x - target.position.x) > Mathf.Abs(currPosition.y - target.position.y) * 0.4f)
            {
                spanim.SetFloat("Xvel", 1f);
            }
            else if (target.position.x - currPosition.x  < -2f && Mathf.Abs(currPosition.x - target.position.x) > Mathf.Abs(currPosition.y - target.position.y) * 0.4f)
            {
                spanim.SetFloat("Xvel", -1f);
            }
            else
            {
                spanim.SetFloat("Xvel", 0f);
            }
            if (target.position.y - currPosition.y > 2f && Mathf.Abs(currPosition.y - target.position.y) > Mathf.Abs(currPosition.x - target.position.x) * 0.4f)
            {
                spanim.SetFloat("Yvel", 1f);
            }
            else if (target.position.y -currPosition.y < -2f && Mathf.Abs(currPosition.y - target.position.y) > Mathf.Abs(currPosition.x - target.position.x) * 0.4f)
            {
                spanim.SetFloat("Yvel", -1f);
            }
            else
            {
                spanim.SetFloat("Yvel", 0f);
            }
            navi.SetDestination(target.position);
            PerformBoneAttack();
        }
        else if (currState == State.Wander)
        {   
            if (time >= time_move){
                wander();
                time = 0;
            }
            if (currPosition.x - prevX > 0.001f && Mathf.Abs(currPosition.x - prevX) > Mathf.Abs(currPosition.y - prevY) * 0.4f)
            {
                spanim.SetFloat("Xvel", 1f);
            }
            else if (currPosition.x - prevX < -0.001f && Mathf.Abs(currPosition.x - prevX) > Mathf.Abs(currPosition.y - prevY) * 0.4f)
            {
                spanim.SetFloat("Xvel", -1f);
            }
            else
            {
                spanim.SetFloat("Xvel", 0f);
            }
            if (currPosition.y - prevY > 0.001f && Mathf.Abs(currPosition.y - prevY) > Mathf.Abs(currPosition.x - prevX) * 0.4f)
            {
                spanim.SetFloat("Yvel", 1f);
            }
            else if (currPosition.y - prevY < -0.001f && Mathf.Abs(currPosition.y - prevY) > Mathf.Abs(currPosition.x - prevX) * 0.4f)
            {
                spanim.SetFloat("Yvel", -1f);
            }
            else
            {
                spanim.SetFloat("Yvel", 0f);
            }
        }
            if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformBoneAttack();
        }
        // if (playerInRange)
        // {
        //     // If player is in proximity, get up and become vulnerable
        //     SetCrumpledState(false);

        //     // Check if it's time to perform a bone attack
        //     if (Time.time - lastBoneAttackTime >= boneAttackCooldown)
        //     {
        //         PerformBoneAttack();
        //     }

        //     // Check if it's time to perform a finger attack
        //     if (Time.time - lastFingerAttackTime >= fingerAttackCooldown)
        //     {
        //         PerformFingerAttack();
        //     }
        // }

        // if (isInvincible)
        // {
        //     if (Time.time > timeToRecover)
        //     {
        //         // Skeleton becomes vulnerable again
        //         isInvincible = false;
        //         skeletonAnimator.SetTrigger("Recover");
        //     }
        // }
    }

    void wander()
    {
        //float oldPosX = currPosition.x;
        PositionChange();
        //flipSprite(oldPosX);
    }

    void chasing(float distance)
    {
        float frame_speed = speed * Time.deltaTime;
        //angle = Mathf.Atan2((target.position.y - currPosition.y) , (target.position.x - currPosition.x));
        //Vector2 conversion = currPosition;
        //navi.SetDestination(conversion);
        transform.position = Vector2.MoveTowards(transform.position,target.position, frame_speed);
        //print("Debug: speed is" + speed);
       // print("Debug: speed per frame is" + frame_speed);
        //flipSprite(oldPosX);
    }

    public override void PositionChange()
    { 
        float posxmin = transform.position.x - speed;
        float posxmax = transform.position.x + speed;
        float posymin = transform.position.y - speed;
        float posymax = transform.position.y + speed;
        currPosition = new Vector2(Random.Range(posxmin, posxmax), Random.Range(posymin, posymax));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        navi.isStopped = true;
        if (collision.gameObject.tag == "Player")
        {
            navi.isStopped = false;
            //bounce();
        }
    }
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currHealth -= damage;

            if (currHealth <= 0)
            {
                // Skeleton is defeated
                Defeat();
            }
            else
            {
                // Skeleton crumbles
                isInvincible = true;
                timeToRecover = Time.time + invincibilityDuration;
                skeletonAnimator.SetTrigger("Crumble");
            }
        }
    }

    void Defeat()
    {
        // Handle skeleton's defeat logic (e.g., drop items, play animation, etc.)
        // This is where you would handle permanent defeat if a strong disintegration attack is used.
    }

    void SetCrumpledState(bool crumpled)
    {
        skeletonAnimator.SetBool("Crumpled", crumpled);
    }


    void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Enemy"))
    {
        // Check if the collided object is an enemy
        BaseSkeleton enemy = other.GetComponent<BaseSkeleton>();

        if (enemy != null)
        {
            // Deal damage to the enemy
            enemy.TakeDamage(damage);
        }
    }
}


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }




void PerformBoneAttack()
{
    if (bonePrefab != null && boneSpawnPoint != null)
    {
        // Instantiate the bone prefab at the spawn point
        GameObject bone = Instantiate(bonePrefab, boneSpawnPoint.position, boneSpawnPoint.rotation);

        // Calculate the direction from the bone to the player
        Vector3 directionToPlayer = (target.position - boneSpawnPoint.position).normalized;

        // Get the Rigidbody component of the bone
        Rigidbody boneRigidbody = bone.GetComponent<Rigidbody>();

        if (boneRigidbody != null)
        {
            // Apply force to the bone in the direction of the player
            boneRigidbody.AddForce(directionToPlayer * boneSpeed, ForceMode.Impulse);
        }

        // Set the last attack time
        lastBoneAttackTime = Time.time;
    }
}





    void PerformFingerAttack()
    {
        if (fingerPrefab != null && fingerSpawnPoint != null)
        {
            // Instantiate and shoot a finger projectile
            GameObject finger = Instantiate(fingerPrefab, fingerSpawnPoint.position, fingerSpawnPoint.rotation);
            Rigidbody fingerRigidbody = finger.GetComponent<Rigidbody>();

            if (fingerRigidbody != null)
            {
                // Apply force to the finger in the forward direction
                fingerRigidbody.AddForce(fingerSpawnPoint.forward * 20.0f, ForceMode.Impulse);

                // Set the last attack time
                lastFingerAttackTime = Time.time;
            }
        }
    }

    void StateChange()
    {
        //
        
        if (distance <= alertRange && !playerSighted)
        {
            playerSighted = true;
            currState = State.Chasing;
           // flipSprite(-target.position.x);
        }
        else if(distance >= alertRange)
        {
            playerSighted = false;
            currState = State.Wander;
        }
    }
}

