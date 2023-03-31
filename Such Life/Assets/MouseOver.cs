using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class MouseOver : MonoBehaviour
{

    public void Awake(){
        Debug.Log("You Mean I Wasted My Tomato Sauce?");
        
    }

    public void TooltipOn(){
        Debug.Log("Hello!");
    }
 
    public void TooltipOff(){
        Debug.Log("Goodbye!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Quad1")
        {
            Debug.Log("Do something here 1");
        }
        if (collision.gameObject.name == "Quad2")
        {
            Debug.Log("Do something here 2");
        }
        if (collision.gameObject.name == "Quad3")
        {
            Debug.Log("Do something here 3");
        }
        if (collision.gameObject.name == "Quad4")
        {
            Debug.Log("Do something here 4");
        }
    }

}
