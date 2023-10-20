using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySkeletonBowman : BaseSkeleton
{

	
	    
	void Awake()
	{
	// You can keep any additional initialization specific to this skeleton type here.
	}

	void Start()
	{
		maxHealth = 50;

		invincibilityDuration = 1.0f;
		isInvincible = false;

		timeToRecover = 0;

		proximityDetectionDistance = 3.0f;
		playerInRange = false;

		//public GameObject bonePrefab; // Prefab for the bone projectile
		//public Transform boneSpawnPoint; // Transform where bones are spawned
		boneAttackCooldown = 4.0f;
		lastBoneAttackTime = 0;

		//public GameObject fingerPrefab; // Prefab for the finger projectile
		//public Transform fingerSpawnPoint; // Transform where fingers are spawned
		fingerAttackCooldown = 8.0f;
		lastFingerAttackTime = 0;
		// You can set specific starting values for this skeleton type if needed.
	}
}
