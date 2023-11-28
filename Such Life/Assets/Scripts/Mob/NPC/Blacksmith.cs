using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

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
        dialogue.Add("Temporary1",  "Hello" );
        dialogue.Add("Temporary2", "I is Blacksmith");
        navi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("MC");
    }

    void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(findClosestObj("MC", 0.5f))
        {
            if (Interactable)
            {
                
            }
        }
    }

    public void createWeapon()
    {

    }

    public void speak()
    {

    }

}
