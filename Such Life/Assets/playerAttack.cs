using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerAttack : MonoBehaviour
{

    [SerializeField]
    private InputActionReference attack;
    private weaponParent wparent;
    

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    private void OnDisable(){
        attack.action.performed -= PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext obj){
        wparent.AttackFunct();
    }

    private void Awake(){
        wparent = GetComponent<weaponParent>();
    }

    
}
