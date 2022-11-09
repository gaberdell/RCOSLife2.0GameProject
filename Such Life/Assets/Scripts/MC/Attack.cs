using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public playerAction playerControls;
    private InputAction fire;

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new playerAction();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }
    private void OnDisable()
    {
        fire.Disable();

    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Attack input registered");
    }
}
