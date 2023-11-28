using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
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

    private void OnEnable()
    {
        EventManager.onDealDamage += TakeDamage;
    }

        // Play hurt animation
        animator.SetTrigger("IsHit");
        if(currentHealth <= 0)
        {
            currentHealth -= (int) damage;

            // Play hurt animation
            animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                Die();
            }
            return true;
        }
        return false;
    }

    void Die()
    {
        if (!isDead){
            Debug.Log("Enemy died!");
            //Die animation
            animator.SetBool("IsDead",true);
            //Disable the enemy
            GetComponent<Collider2D>().enabled = false;
            GetComponent<EnemyMovement>().enabled = false;
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
