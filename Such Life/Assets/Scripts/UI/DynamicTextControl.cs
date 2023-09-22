using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicTextControl : MonoBehaviour
{
    public GameObject DynInven;
    Vector3[] v = new Vector3[4];

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GrabCorners()
    {
        DynInven.GetComponent<RectTransform>().GetWorldCorners(v);
        transform.position = v[1];
    }

    // Update is called once per frame
    void Update()
    {
        GrabCorners();
        if(transform.position != v[1]){
            transform.position = v[1];
        }
    }



}
