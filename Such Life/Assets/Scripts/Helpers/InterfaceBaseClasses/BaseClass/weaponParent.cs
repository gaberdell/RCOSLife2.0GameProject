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
    public InputAction attack;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        charRenderer = gameObject.GetComponent<SpriteRenderer>();
        weaponRenderer = gameObject.GetComponent<SpriteRenderer>();
        controls = new playerAction();
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

    public void AttackFunct(){
        anim.SetTrigger("Attack");
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack(){
        yield return new WaitForSeconds(delay);
    }

    
}
