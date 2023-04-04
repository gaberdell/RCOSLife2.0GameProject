using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class MouseOver : MonoBehaviour
{
    public int quad = 0;  
    Vector3 pos;
    public void Awake(){
        pos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }

    public void TooltipOn(){
        Debug.Log("Hello!");
    }
 
    public void TooltipOff(){
        Debug.Log("Goodbye!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Quad1" && quad == 0)
        {
            quad = 1;
            transform.position = new Vector3(pos.x-7.7f,pos.y-2.9f,pos.z);
        }
        else if (collision.gameObject.name == "Quad2" && quad == 0)
        {
            quad = 2;
            transform.position = new Vector3(pos.x-7.7f,pos.y+18.0f,pos.z);
        }
        else if (collision.gameObject.name == "Quad3" && quad == 0)
        {
            quad = 3;
            transform.position = new Vector3(pos.x+17.8f,pos.y+18.0f,pos.z);
        }
        else if (collision.gameObject.name == "Quad4" && quad == 0)
        {
            quad = 4;
            transform.position = new Vector3(pos.x+17.8f,pos.y-2.9f,pos.z);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        transform.position = pos;
        quad = 0;
    }

}
