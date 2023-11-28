using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    public GameObject easySkeletonPrefab;
    public GameObject mediumSkeletonPrefab;
    public GameObject hardSkeletonPrefab;
    public GameObject flyingSkeletonPrefab; // Add the FlyingSkeleton prefab

    public Transform spawnPoint;
    public float spawnInterval = 10.0f;

    private float timeSinceLastSpawn = 0;

    // Adjust the spawn probabilities
    [Range(0, 1)]
    public float easySpawnProbability = 0.4f; // Adjust probabilities as needed
    [Range(0, 1)]
    public float mediumSpawnProbability = 0.3f;
    [Range(0, 1)]
    public float flyingSpawnProbability = 0.2f; // Probability for the FlyingSkeleton

    private void Start()
    {
        // Initialize the timer to allow the first spawn immediately
        timeSinceLastSpawn = spawnInterval;
    }

    private void Update()
    {
        // Update the timer
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (timeSinceLastSpawn >= spawnInterval)
        {
            // Randomly select an enemy type to spawn based on probabilities
            GameObject enemyToSpawn = SelectRandomEnemyPrefab();

            // Spawn the selected enemy
            if (enemyToSpawn != null)
            {
                Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
            }

            // Reset the timer
            timeSinceLastSpawn = 0;
        }
    }

    GameObject SelectRandomEnemyPrefab()
    {
        // Generate a random number to select the enemy type
        float randomValue = Random.value;

        if (randomValue < easySpawnProbability)
        {
            return easySkeletonPrefab;
        }
        else if (randomValue < easySpawnProbability + mediumSpawnProbability)
        {
            return mediumSkeletonPrefab;
        }
        else if (randomValue < easySpawnProbability + mediumSpawnProbability + flyingSpawnProbability)
        {
            return flyingSkeletonPrefab; // Spawn the FlyingSkeleton
        }
        else
        {
            return hardSkeletonPrefab;
        }
    }
}

