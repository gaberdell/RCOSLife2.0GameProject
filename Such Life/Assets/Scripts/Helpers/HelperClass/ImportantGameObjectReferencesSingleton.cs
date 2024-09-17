using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Just a class because unity cant properly handle statics being serailized because its a sussy;
public class ImportantGameObjectReferencesSingleton : MonoBehaviour
{

    public static GameObject DynText { get; private set; }

    [SerializeField]
    GameObject DynTextSerializeField;


    private void Awake()
    {
        DynText = DynTextSerializeField;
    }
}
