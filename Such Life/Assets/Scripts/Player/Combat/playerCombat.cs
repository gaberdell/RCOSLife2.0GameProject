using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    
    // Update is called at every frame update
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
        }
        
        
    }

    void Attack()
    {
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
