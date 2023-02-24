using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class HitEmHard : MonoBehaviour
{
    [SerializeField]
    private InputActionReference hit;


    private weaponParent wparent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onEnable() {
        attack.action.performed += PerformAttack;
    }

    private void onDisable() {
        attack.action.performed -= PerformAttack;
    }
}
