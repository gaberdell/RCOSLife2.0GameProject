using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHP : MonoBehaviour
{

    public float maxHP = 100;
    private float currHP;
    public Animator animator;
    public healthBarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        healthBar.SetMaxHealth(maxHP);
    }

    void Update()
    {
        
    }


    public void decHP(float decAM){

        // Play hurt animation
        //animator.SetTrigger("Hurt");

        currHP -= decAM;

        healthBar.SetHealth(currHP);

        if(currHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        //Die animation
        animator.SetBool("IsDead",true);
        //Disable the player
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }
    
}