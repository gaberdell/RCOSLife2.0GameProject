using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SetID : MonoBehaviour
{
    [SerializeField] private string _id = Guid.NewGuid().ToString();
    //[TAG FOR REMOVAL] Try to centralize or remove this stuff doesn't really have a use??!?
    //[SerializeField] private static SerializableDictionary<string, GameObject> idDatabase = new SerializableDictionary<string, GameObject>();

    //This is just here sense its faster for local scripts in case they need to grab it
    public string ID => _id;

    private void OnEnable()
    {
        EventManager.getID += returnID;
        EventManager.forceIDValidation += forceValidate;
    }

    private void OnDisable()
    {
        EventManager.getID -= returnID;
        EventManager.forceIDValidation -= forceValidate;
    }


    private string returnID(GameObject isOurGameObject, ref string id)
    {
        if (isOurGameObject == gameObject)
        {
            id = _id;
            return _id;
        }
        return null;
    }

    private void forceValidate(GameObject isOurGameObject)
    {
        if (isOurGameObject == gameObject)
            OnValidate();
    }

    private void OnValidate()
    {
        //just here so the old code base doesn't throw a fit
    }
}
