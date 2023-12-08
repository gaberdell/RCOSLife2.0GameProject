using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescripChange : MonoBehaviour
{
    public TMP_Text myText;

    // Update is called once per frame
    void Update()
    {
        myText.text = gameObject.transform.parent.gameObject.GetComponent<mouseBoxFollow>().ItemDescrip;
    }
}
