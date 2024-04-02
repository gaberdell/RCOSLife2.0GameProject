using System;
using System.Collections;
using UnityEngine;

/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/


[System.Serializable]
//[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [SerializeField]
    private bool _isSetID = false;

    [ReadOnly, SerializeField] private string _id = Guid.NewGuid().ToString();
    //[TAG FOR REMOVAL] Try to centralize or remove this stuff doesn't really have a use??!?
    [SerializeField] private static SerializableDictionary<string, GameObject> _idDatabase = new SerializableDictionary<string, GameObject>();

    //This is just here sense its faster for local scripts in case they need to grab it
    public string ID => _id;

    private void Awake()
    {
        if (!_isSetID) {
            _id = Guid.NewGuid().ToString();
        }
    }


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
        //if the database contains the id, generate a new one, or else add the id into the database
        //objects in the world will be linked to the ID
        if (_idDatabase.ContainsKey(_id))
        {
            Generate();
        }
        else
        {
            _idDatabase.Add(_id, this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //if the objects already remove from the world, remove it from the database
        if (_idDatabase.ContainsKey(_id))
        {
            _idDatabase.Remove(_id);
        }
    }


    private void Generate()
    {
        _id = Guid.NewGuid().ToString();
        _idDatabase.Add(_id, this.gameObject);
    }


}
