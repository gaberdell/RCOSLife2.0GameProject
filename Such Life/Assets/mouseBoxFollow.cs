using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class mouseBoxFollow : MonoBehaviour
{
    protected string itemName = "(No Item)";
    protected string itemDescrip = "An empty item slot, what did you expect?";
    public string ItemName => itemName;
    public string ItemDescrip => itemDescrip;

    void Start(){
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = Input.mousePosition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BoxTag"){
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.GetComponent<Image>().enabled = true;
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
            this.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
