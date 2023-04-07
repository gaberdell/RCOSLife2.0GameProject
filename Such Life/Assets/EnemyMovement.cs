using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float speed = 0.5f;
    public Transform Player;
    public PlayerHP pHP;
    
    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        Vector3 displacement = Player.position - transform.position;
        displacement = displacement.normalized;
        if (Vector2.Distance (Player.position, transform.position) > 1.0f) {
            sprite.color = Color.red;
            sprite.color = Color.white;
            transform.position += (displacement * speed * Time.deltaTime);
                        
        }
        else{
            Player.position += (displacement);
            sprite.color = Color.red;
            pHP.decHP(20);
        }
    }

}
