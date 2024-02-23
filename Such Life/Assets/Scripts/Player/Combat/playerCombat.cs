using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 40;
    [SerializeField] private float AttackRate = 2f;
    private float NextAttackTime = 0f;
    
    // Update is called at every frame update
    void Update() {
        if(Time.time >= NextAttackTime && Input.GetKeyDown(KeyCode.Space)) { 
            Attack();
            NextAttackTime = Time.time + 1f / AttackRate;     
        }
    }

    void Attack() {
        // Play attack animation
        animator.SetTrigger("Attack");
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,enemyLayers);
        Debug.Log(hitEnemies.Length);
        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit"+ enemy.name);
            EventManager.DealDamage(enemy.gameObject, attackDamage);
        }
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.blue;
        Vector3 position = attackPoint == null ? Vector3.zero: attackPoint.position;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
