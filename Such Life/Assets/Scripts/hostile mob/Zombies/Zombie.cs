using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : mobBase
{
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 5000;
        currHealth = maxHealth;
        damage = 1000;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
