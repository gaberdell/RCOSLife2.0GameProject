using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class <c>mouseBoxFollow</c> Class that sets the tooltip for the for the item
///                             the player is viewing by taking the name and the
///                             description. Addionally it sets the position of
///                             the object to the proper position.
///                                      
/// Relationship status : 
/// <c>MonoBehaviour</c> based class.
/// <c>InventorySlot_UI</c> Uses this to grab the information to display.
/// </summary>
public class mouseBoxFollow : MonoBehaviour
{
    protected string itemName = "(No Item)";
    protected string itemDescrip = "An empty item slot, what did you expect?";
    public string ItemName => itemName;
    public string ItemDescrip => itemDescrip;

    void Start(){
        //[FIXED]
        //Blud this is awful coding
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Bro :skull: this is wasteful coding
        this.gameObject.transform.position = Input.mousePosition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "BoxTag"){
            //Debug.Log(collision.name);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            if(collision.gameObject.GetComponent<InventorySlot_UI>().AssignedInventorySlot.ItemData != null){
                itemName = collision.gameObject.GetComponent<InventorySlot_UI>().AssignedInventorySlot.ItemData.DisplayName;
                itemDescrip = collision.gameObject.GetComponent<InventorySlot_UI>().AssignedInventorySlot.ItemData.Description;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BoxTag"){
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            //this.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
