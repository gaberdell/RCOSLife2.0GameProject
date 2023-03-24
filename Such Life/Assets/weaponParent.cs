using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class weaponParent : MonoBehaviour
{
    public SpriteRenderer charRenderer, weaponRenderer;

    public Vector2 PointerPosition {get; set; }

    public Animator anim;

    public playerAction controls;
    private InputAction attack;
    private int delay = 1;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        charRenderer = gameObject.GetComponent<SpriteRenderer>();
        weaponRenderer = gameObject.GetComponent<SpriteRenderer>();
        controls = new playerAction();
        anim.SetBool("Exists", true);
        anim.SetBool("IHit", false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (PointerPosition + (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if(direction.x < 0) {
            scale.y = -1;
        }
        else if(direction.x > 0) {
            scale.y = 1;
        }
        transform.localScale = scale;

        if(transform.eulerAngles.z >0 && transform.eulerAngles.z < 180){
            weaponRenderer.sortingOrder = charRenderer.sortingOrder - 1;
        }
        else {
            weaponRenderer.sortingOrder = charRenderer.sortingOrder + 1;
        }
        


    }

    public void AttackFunct(InputAction.CallbackContext context){
        if(context.started){
            anim.SetTrigger("Attack");
            Debug.Log("Attacked the Enemy");
        }
        if(context.canceled){
            StartCoroutine(dee_delay());
            anim.SetBool("IHit", true);
        }
    }

    private IEnumerator dee_delay(){
        yield return new WaitForSeconds(delay);
    }

    private void onEnable(){
        attack = controls.Player.Attacking;
        controls.Player.Enable();
        attack.Enable();
        attack.performed += AttackFunct;
    }

    private void onDisable(){
        controls.Player.Disable();
        attack.Disable();
    }

    
}
