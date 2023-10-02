using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collision2D),typeof(NavMeshModifier))]
public class NavMeshSlowDown : MonoBehaviour{
    private NavMeshModifier mod;
    private void Awake(){
        mod = GetComponent<NavMeshModifier>();
    }

    /*private void OnCollisionEnter2D(Collision2D other){
        float CostModifier = NavMesh.GetAreaCost(mod.area);
        Rigidbody2D agent = other.gameObject.GetComponent<Rigidbody2D>();
        agent.velocity /= CostModifier;
       
    }*/
    private void OnTriggerEnter2D(Collider2D col){
        float CostModifier;
        switch(mod.area){
            case 3: CostModifier = 2.0F; break;
            case 4: CostModifier = 4.0F; break;
            default: CostModifier = 1.0F; break;
        }
        Debug.Log("Enter");
        if(col.gameObject.tag == "Player"|| col.gameObject.tag == "Untagged"){
            
            col.gameObject.GetComponent<PlayerMovement>().walkSpeed /= CostModifier;
            
           
        }
    }
    private void OnTriggerExit2D(Collider2D col){
        float CostModifier;
        switch(mod.area){
            case 3: CostModifier = 2.0F; break;
            case 4: CostModifier = 4.0F; break;
            default: CostModifier = 1.0F; break;
        }
        Debug.Log("Exit");
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "Untagged"){
            col.gameObject.GetComponent<PlayerMovement>().walkSpeed *= CostModifier;
        }
    }
    /*private void OnCollisionExit2D(Collision2D other){
        float CostModifier = NavMesh.GetAreaCost(mod.area);
        Rigidbody2D agent = other.gameObject.GetComponent<Rigidbody2D>();
        agent.velocity *= CostModifier;
        
    }*/
}