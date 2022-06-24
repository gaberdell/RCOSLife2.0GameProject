using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slim : mobBase
{
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 5;
        damage = 2;
        currHealth = maxHealth;
        walkSpeed = 1f;
        currPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
