using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        EventManager.onDealDamage += TakeDamage;
    }

    private void OnDisable()
    {
        EventManager.onDealDamage -= TakeDamage;
    }

    public bool TakeDamage(GameObject ourGameObject, float damage)
    {
        if (ourGameObject == gameObject)
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
        Debug.Log("Enemy died!");
        //Die animation
        animator.SetBool("IsDead",true);
        //Disable the enemy
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        //GetComponent<SpriteRenderer>().enabled = false;
        // Destroy after death animation
        Destroy(gameObject, 1.0f);
    }
    
}
