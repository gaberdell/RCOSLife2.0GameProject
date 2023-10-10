using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Private Variables
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private Transform Player;
    [SerializeField] private float damageAmount = 20f;

    // Update is called once per frame
    private void Update () {
        Vector3 displacement = Player.position - transform.position;
        displacement = displacement.normalized;
        if (Vector2.Distance (Player.position, transform.position) > 1.0f) {
            sprite.color = Color.red;
            sprite.color = Color.white;
            transform.position += (displacement * speed * Time.deltaTime);
                        
        }
        else {
            Player.position += (displacement);
            sprite.color = Color.red;
            EventManager.DealDamage(Player.gameObject, damageAmount);
        }
    }

}
