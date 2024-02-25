using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Class <c>NameChange</c> Text Mesh Pro of its parent mouse box follow and sets the text to the item name every frame.
/// Relationship status : 
/// <c>MonoBehaviour</c> based class
/// <c>mouseBoxFollow</c> is what it iteracts with
/// Similar to <c>DescripChange</c>
/// /// </summary>

//POSSIBLE OPTIMIZATION (check when mouse moves then update?)
public class NameChange : MonoBehaviour
{
    public TMP_Text myText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = gameObject.transform.parent.gameObject.GetComponent<mouseBoxFollow>().ItemName;
    }
}
