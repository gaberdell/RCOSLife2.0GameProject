using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class MouseOver : MonoBehaviour
{
    public int quad = 0;  
    Vector3 pos;
    bool moveable = true;
    GameObject my_parent;
    protected string itemName = "(No Item)";
    protected string itemDescrip = "An empty item slot, what did you expect?";
    public string ItemName => itemName;
    public string ItemDescrip => itemDescrip;

    public void Awake(){
        pos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        my_parent = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        itemName = "(Placeholder)";
        try{
            string test = my_parent.GetComponent<InventorySlot_UI>().AssignedInventorySlot.ItemData.DisplayName;
        }
        catch(NullReferenceException err){
            itemName = "(No Item)";
        }
        if(itemName == "(Placeholder)"){
            itemName = my_parent.GetComponent<InventorySlot_UI>().AssignedInventorySlot.ItemData.DisplayName;
        }
        Debug.Log(itemName);

        itemDescrip = "(Placeholder)";
        try{
            string test2 = my_parent.GetComponent<InventorySlot_UI>().AssignedInventorySlot.ItemData.Description;
        }
        catch(NullReferenceException err){
            itemDescrip = "An empty item slot, what did you expect?";
        }
        if(itemDescrip == "(Placeholder)"){
            itemDescrip = my_parent.GetComponent<InventorySlot_UI>().AssignedInventorySlot.ItemData.Description;
        }
        Debug.Log(itemDescrip);

        if (collision.gameObject.name == "Quad1" && quad == 0 && moveable)
        {
            quad = 1;
            transform.position = new Vector3(pos.x-7.7f,pos.y-2.9f,pos.z);
        }
        else if (collision.gameObject.name == "Quad2" && quad == 0 && moveable)
        {
            quad = 2;
            transform.position = new Vector3(pos.x-7.7f,pos.y+18.0f,pos.z);
        }
        else if (collision.gameObject.name == "Quad3" && quad == 0 && moveable)
        {
            quad = 3;
            transform.position = new Vector3(pos.x+17.8f,pos.y+18.0f,pos.z);
        }
        else if (collision.gameObject.name == "Quad4" && quad == 0 && moveable)
        {
            quad = 4;
            transform.position = new Vector3(pos.x+17.8f,pos.y-2.9f,pos.z);
        }
    }

    void OnTriggerStay2D(Collider2D collision){
       moveable = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        transform.position = pos;
        quad = 0;
        moveable = true;
    }

}
