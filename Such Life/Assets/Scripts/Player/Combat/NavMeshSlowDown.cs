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

    private void OnTriggerEnter2D(Collider2D col){
        float CostModifier;
        switch(mod.area){
            case 3: CostModifier = 2.0F; break;
            case 4: CostModifier = 4.0F; break;
            default: CostModifier = 1.0F; break;
        }
        Debug.Log("Enter");
        if(col.gameObject.tag == "Player"|| col.gameObject.tag == "Untagged"){
            EventManager.SetPlayerWalkSpeed(EventManager.GetPlayerWalkSpeed() / CostModifier);
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
            EventManager.SetPlayerWalkSpeed(EventManager.GetPlayerWalkSpeed() * CostModifier);
        }
    }
}