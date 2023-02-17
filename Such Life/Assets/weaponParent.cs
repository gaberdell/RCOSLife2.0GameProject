using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponParent : MonoBehaviour
{

    public SpriteRenderer charRenderer, weaponRenderer;

    public Vector2 PointerPosition {get; set; }

    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
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


    public void Attack() {
        if(attackBlocked)
            return;
        animmator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack() {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    }
}
