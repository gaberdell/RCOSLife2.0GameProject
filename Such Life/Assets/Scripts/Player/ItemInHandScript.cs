using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Basic ahh script makes it so the it
public class ItemInHandScript : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private const float _radiansToDegrees = 57.2f;

    private void OnEnable()
    {
        EventManager.currentlySelectedItem += UpdateRenderForNewItem;
    }

    private void OnDisable()
    {
        EventManager.currentlySelectedItem -= UpdateRenderForNewItem;
    }



    private void UpdateRenderForNewItem(InventoryItemData selectedItem)
    {
        if (selectedItem)
            spriteRenderer.sprite = selectedItem.Icon;
        else
            spriteRenderer.sprite = null;

    }

    private void Start()
    {
        spriteRenderer.sprite = null;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;

        float signedAngle = Vector2.SignedAngle(Vector2.up, direction);

        if (signedAngle < -60.6 || signedAngle > 150)
        {
            spriteRenderer.sortingOrder = 1;
        }
        else
        {
            spriteRenderer.sortingOrder = -1;
        }

        //Big ahh function makes an atan to get the proper rotation based on the turn
        transform.eulerAngles = new Vector3(0,0, signedAngle);
    }
}
