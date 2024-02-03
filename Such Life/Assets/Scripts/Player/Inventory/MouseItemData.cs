using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/* Codes provided by: Dan Pos - Game Dev Tutorials! */
/// <summary>
/// Class <c>Mouse Item Data</c> job is to keep track of when a player picks up an item.
/// Relationship status : 
/// <c>MooBehaviour</c> based class
/// <c>InventoryDisplay</c> what it mainly iterfaces with.
/// Further noting on <c>Mouse Item Data</c> with <c>InventoryDisplay</c> is that the
/// child of the latter <c>StaticInventoryDisplay</c> houses a collection of slots
/// that use the knowledge of the curretly picked up item to transfer items.
/// </summary>

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;

    private Transform _playerTransform;
    private Camera my_cam;

    private void Awake()
    {
        if (ItemSprite != null)
        {
            ItemSprite.color = Color.clear;
            ItemSprite.preserveAspect = true;
        }
        ItemCount.text = "";

        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        my_cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if(_playerTransform == null) Debug.Log("Player not found");
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        AssignedInventorySlot.AssignItem(invSlot); //make the mouse holds the item
        ItemSprite.sprite = invSlot.ItemData.Icon; //update icon
        ItemCount.text = invSlot.StackSize.ToString();
        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        // To-do: Add controller support later down the lines
        //If there is an item in the mouse inventory, make the item follow the mouse
        if(AssignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();
            if(Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                if(AssignedInventorySlot.ItemData.ItemPrefab != null){
                    Vector3 newPos = my_cam.ScreenToWorldPoint(Input.mousePosition);
                    newPos.z = 0.0f;
                    for(int i = 0; i < AssignedInventorySlot.StackSize-1; i++){
                        if(i%2 == 0){
                            Instanter(AssignedInventorySlot,newPosShift(newPos,true));
                        }
                        else{
                            Instanter(AssignedInventorySlot,newPosShift(newPos,false));
                        }
                    }
                    Instanter(AssignedInventorySlot,newPos);
                }
                ClearSlot();
            }

        }
    }

    private void Instanter(InventorySlot AssignedInventorySlot, Vector3 newPos){
        EventManager.ForceIDValidation(AssignedInventorySlot.ItemData.ItemPrefab);
        GameObject secret_obj = Instantiate(AssignedInventorySlot.ItemData.ItemPrefab, newPos,Quaternion.identity);
        Collider2D secret_collider = secret_obj.GetComponent<Collider2D>();
        secret_collider.enabled = false;
        secret_collider.enabled = true;
    }

    //I have no clue what the hell this is doing???
    //WHY DOES THIS EXSIST IS THIS LIKE AN APRIL FOOLS JOKE
    //WHY IS THE STUFF MADE RANDOM
    public Vector3 newPosShift(Vector3 Pos, bool doNegative){
        Vector3 nextPos = new Vector3(Pos.x, Pos.y,0.0f);
        float randomChoice = Random.Range(0.3f,0.4f);
        if(doNegative){
            randomChoice = Random.Range(-0.4f,-0.3f);
        }
        nextPos.x += randomChoice;
        return nextPos;
    }

    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    //modified from StackOverflow
    public static bool IsPointerOverUIObject()
    {
        // Do a ray-cast on the mouse to see if it's ontop of any object, if yes, return true, else is false
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        Debug.Log("Wow!");
        return results.Count > 0;
    }


}
