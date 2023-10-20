using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseSkeleton : mobBase
{

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

    protected virtual void Awake()
    {
        skeletonAnimator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        currHealth = maxHealth;
        SetCrumpledState(true); // Initially in crumpled state
    }

    protected virtual void Update()
    {
        if (playerInRange)
        {
            // If player is in proximity, get up and become vulnerable
            SetCrumpledState(false);

            // Check if it's time to perform a bone attack
            if (Time.time - lastBoneAttackTime >= boneAttackCooldown)
            {
                PerformBoneAttack();
            }

            // Check if it's time to perform a finger attack
            if (Time.time - lastFingerAttackTime >= fingerAttackCooldown)
            {
                PerformFingerAttack();
            }
        }

        if (isInvincible)
        {
            if (Time.time > timeToRecover)
            {
                // Skeleton becomes vulnerable again
                isInvincible = false;
                skeletonAnimator.SetTrigger("Recover");
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
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
            // Instantiate and throw a bone projectile
            GameObject bone = Instantiate(bonePrefab, boneSpawnPoint.position, boneSpawnPoint.rotation);
            Rigidbody boneRigidbody = bone.GetComponent<Rigidbody>();

            if (boneRigidbody != null)
            {
                // Apply force to the bone in the forward direction
                boneRigidbody.AddForce(boneSpawnPoint.forward * 10.0f, ForceMode.Impulse);

                // Set the last attack time
                lastBoneAttackTime = Time.time;
            }
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
}

