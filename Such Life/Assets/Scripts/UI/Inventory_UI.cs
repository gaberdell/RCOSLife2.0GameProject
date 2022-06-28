using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{   
    public GameObject InventroyScreen;


    public void OpenInventory(){
        //turn "on" and "off" the inventory game Object
        if(!InventroyScreen.activeInHierarchy){
            InventroyScreen.SetActive(true);
        } else {
            InventroyScreen.SetActive(false);
        }
    }



    // Update is called once per frame
    void Update()
    {   
        //let the player bind the key
        if(Input.GetKeyDown("e")) {
            //open the inventoryUi
            OpenInventory();


            
        }
    }
}
