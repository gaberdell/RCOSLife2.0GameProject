using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*The Zombie mob is a very simple mob that approaches the player when the player is within its range.
The Zombie is decently tanky and has high health, attack, and defense; it gets tankier when it falls below
half health. Its only method of attack is by touching the player*/
public class Zombie : mobBase
{

    // Start is called before the first frame update
    void Start()
    {

        maxHealth = 5000;
        currHealth = maxHealth;
        damage = 1000;
        critChance = 0.15f;
        critDamage = 1.5f;
        defense = 750;
        speed = 1f;
        player = GameObject.Find("MC");
        Sprite = GetComponent<SpriteRenderer>();
        navi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navi.updateRotation = false;
        navi.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}