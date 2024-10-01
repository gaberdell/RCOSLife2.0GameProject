using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintIfObjectIsDestroyedOrDisabled : MonoBehaviour
{
    private void OnDisable()
    {
        //Debug.Log(gameObject.name + " was disabled");
    }
    private void OnDestroy()
    {
        //Debug.Log(gameObject.name + " was destroyed");
    }
}
