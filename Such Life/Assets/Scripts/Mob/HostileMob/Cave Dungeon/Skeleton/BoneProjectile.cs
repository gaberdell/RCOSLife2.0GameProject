using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object tagged as an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(collision.gameObject, damage);
        }

        // Destroy the bone on collision with any object
        Destroy(gameObject);
    }
}
