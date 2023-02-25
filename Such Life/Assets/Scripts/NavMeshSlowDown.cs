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

    private void OnCollisionEnter2D(Collision2D other){
        float CostModifier = NavMesh.GetAreaCost(mod.area);
        Rigidbody2D agent = other.gameObject.GetComponent<Rigidbody2D>();
        agent.velocity /= CostModifier;
        Debug.Log("hi mom");
    }
    private void OnCollisionExit2D(Collision2D other){
        float CostModifier = NavMesh.GetAreaCost(mod.area);
        Rigidbody2D agent = other.gameObject.GetComponent<Rigidbody2D>();
        agent.velocity *= CostModifier;
        
    }
}