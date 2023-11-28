using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
