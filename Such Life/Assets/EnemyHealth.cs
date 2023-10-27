using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation
        //animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead){
            Debug.Log("Enemy died!");
            //Die animation
            //animator.SetBool("IsDead",true);
            //Disable the enemy
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<EnemyMovement>().enabled = false;
            isDead = true;
        }

    }
    
}
