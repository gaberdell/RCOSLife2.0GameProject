using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitEmHard : MonoBehaviour
{
    public playerAction playerControls;
    private InputAction attack;

    private weaponParent wparent;

    public bool animation_go;

    private void OnEnable()
    {
        playerControls.Player.Enable();
        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += PerformAttack;
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
        attack.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControls = new playerAction();
    }


    private void PerformAttack(InputAction.CallbackContext obj) {
     //   wparent.Attack();
    }
}