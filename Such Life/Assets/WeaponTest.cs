using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{

    public Animator anim;
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;

    void Awake(){
        radius = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            anim.SetBool("Attacking", true);
        }
    }
        public void endAttack(){
        anim.SetBool("Attacking", false);
    }
    
    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position,radius,enemies);
        foreach (Collider2D enemyGameObject in enemy){
            Debug.Log("Hit an Enemy");
            enemyGameObject.GetComponent<EnemyHealth>().health -= 10;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
