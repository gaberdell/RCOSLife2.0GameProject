using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : NPCBase
{
    
    // Start is called before the first frame update
    void Start()
    {
        HPCap = 100;
        currHP = 100;
        awareness = 5;
        position = new Vector2(transform.position.x, transform.position.y);
        newposition = position;
        NPCName = "Tom";
        Interactable = true;
        Hostile = Hostility.Peaceful;
        Sprite = GetComponent<SpriteRenderer>();
        dialogue.Add("Temporary1", "Hello");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createWeapon()
    {

    }


}
