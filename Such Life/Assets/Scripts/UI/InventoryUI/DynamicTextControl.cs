using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>DynamicTextControl</c> Simple script gets the RectTransformers
///                                 DynamicInventory corners inventory
///                                 then sets the transform to the top left corner
/// Relationship status : 
/// <c>MonoBehaviour</c> based class
/// <c>MouseItemData</c> is here to check if is no item so functionality between
/// <c>DynamicInventoryDisplay</c> calls this script despite it calling itself every update?!
/// </summary>
public class DynamicTextControl : MonoBehaviour
{
    public GameObject DynInven;
    Vector3[] v = new Vector3[4];


    public void GrabCorners()
    {
        DynInven.GetComponent<RectTransform>().GetWorldCorners(v);
        transform.position = v[1];
    }

    // Update is called once per frame
    //HOLY MOLY this is innefcient bruh what is this code bruh :skull:
    void Update()
    {
        GrabCorners();
        if(transform.position != v[1]){
            transform.position = v[1];
        }
    }



}
