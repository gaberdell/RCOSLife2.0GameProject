using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Plays player's attack animation 
 * interacts with enemies through a weapon
 */
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform weapon;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 40;
    [SerializeField] private float AttackRate = 2f;
    private float NextAttackTime = 0f;
    
    // Update is called at every frame update
    void FixedUpdate() {
        if(Time.time >= NextAttackTime && Input.GetKeyDown(KeyCode.Space)) { 
            Attack();
            NextAttackTime = Time.time + 1f / AttackRate;     
        }
    }

    void Attack() {
        // Play attack animation
        animator.SetTrigger("Attack");
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(weapon.position, attackRange,enemyLayers);
        Debug.Log(hitEnemies.Length);
        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit"+ enemy.name);
            EventManager.DealDamage(enemy.gameObject, attackDamage);
        }
    }

    // create gizmo on weapon
    private void OnDrawGizmosSelected() {
        if (weapon == null)
            return;
        Gizmos.color = Color.blue;
        Vector3 position = weapon == null ? Vector3.zero: weapon.position;
        Gizmos.DrawWireSphere(weapon.position, attackRange);
    }
}
