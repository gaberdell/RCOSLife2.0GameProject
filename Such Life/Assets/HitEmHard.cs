using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class HitEmHard : MonoBehaviour
{
    [SerializeField]
    private InputActionReference hit;

    public playerAction playerControls;
    private InputAction attack;

    private weaponParent wparent;

    public bool animation_go;

    // Start is called before the first frame update
    void Start()
    {

        playerControls = new playerAction();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PerformAttack(InputAction.CallbackContext obj) {
        wparent.Attack();
    }



    private void onEnable() {
        attack.action.performed += PerformAttack;
        playerControls.Player.Enable();
        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += PerformAttack;
    }

    private void onDisable() {
        attack.action.performed -= PerformAttack;
        playerControls.Player.Disable();
        attack.Disable();
    }
}