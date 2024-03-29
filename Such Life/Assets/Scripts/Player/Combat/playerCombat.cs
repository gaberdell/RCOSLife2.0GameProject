using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlasticPipe.Server.MonitorStats;

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

    [SerializeField] private playerAction playerControls;
    [SerializeField] private InputAction attack;
    [SerializeField] private float attackTime; //same variables as dash but for roll
    [SerializeField] private float attackCooldown;
    private float lastAttackUsed;
    private float NextAttackTime = 0f;


    private void Awake()
    {
        playerControls = new playerAction();
        animator = GetComponent<Animator>();

        attackTime = .1f;
        attackCooldown = .5f;
        lastAttackUsed = 0 - attackCooldown;

    }


    // create gizmo on weapon
    private void OnDrawGizmosSelected() {
        if (weapon == null)
            return;
        Gizmos.color = Color.blue;
        Vector3 position = weapon == null ? Vector3.zero: weapon.position;
        Gizmos.DrawWireSphere(weapon.position, attackRange);
    }


    private void OnEnable()
    {
        playerControls.Player.Enable();


        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += AttackFunct;

    }

    // Remove disabled functions
    private void OnDisable()
    {
        playerControls.Player.Disable();
        attack.performed -= AttackFunct;
    }

    private void AttackFunct(InputAction.CallbackContext context)
    {
        StartCoroutine(AttackRoutine());
    }
    // Binded to v
    private IEnumerator AttackRoutine()
    {
        if (Time.time > lastAttackUsed + attackCooldown)
        {
            lastAttackUsed = Time.time;
            Debug.Log("Called attack routine");
            yield return new WaitForSeconds(attackTime);
            yield return null;

            // Play attack animation
            animator.SetTrigger("Attack");
            // Detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(weapon.position, attackRange, enemyLayers);
            Debug.Log(hitEnemies.Length);
            // Damage them
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit" + enemy.name);
                EventManager.DealDamage(enemy.gameObject, attackDamage);
            }

            NextAttackTime = Time.time + 1f / AttackRate;
        }
        else
        {
            float timeRemaining = attackCooldown - (Time.time - lastAttackUsed);
            Debug.Log("Cannot attack: attack is on cooldown. " + timeRemaining.ToString("F2") + "seconds left");
        }


        

        
    }
}
