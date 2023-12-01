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
        animator.SetTrigger("IsHit");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isDead){
            Debug.Log("Enemy died!");
            //Die animation
            animator.SetBool("IsDead",true);
            //Disable the enemy
            GetComponent<Collider2D>().enabled = false;
            if(GetComponent<EnemyMovement>() != null ){
                GetComponent<EnemyMovement>().enabled = false;
            }
            this.enabled = false;
            isDead = true;
        }
        if(isDead){
            StartCoroutine(Dead());
            Dead();            
        }
    }

      IEnumerator Dead()
    {
        // Death animation runs for 1.3 seconds
        yield return new WaitForSeconds(1.3f);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }
    
}
