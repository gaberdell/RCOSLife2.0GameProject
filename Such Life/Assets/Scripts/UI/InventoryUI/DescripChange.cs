using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class <c>DescripChange</c> Text Mesh Pro of its parent mouse box follow and sets the text to the item description every frame.
/// Relationship status : 
/// <c>MonoBehaviour</c> based class
/// <c>mouseBoxFollow</c> is what it iteracts with
/// Similar to <c>NameChange</c>
/// </summary>

//POSSIBLE OPTIMIZATION (check when mouse moves then update?)
public class DescripChange : MonoBehaviour
{
    public TMP_Text myText;

    // Update is called once per frame
    void Update()
    {
        myText.text = gameObject.transform.parent.gameObject.GetComponent<mouseBoxFollow>().ItemDescrip;
    }
}
