using System;
using System.Collections;
using UnityEngine;

/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/


[System.Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField] private string _id = Guid.NewGuid().ToString();
    [SerializeField] private static SerializableDictionary<string, GameObject> idDatabase = new SerializableDictionary<string, GameObject>();

    public string ID => _id;

    public void forceValidate(){
        OnValidate();
    }

    private void OnValidate()
    {
        //if the database contains the id, generate a new one, or else add the id into the database
        //objects in the world will be linked to the ID
        if (idDatabase.ContainsKey(_id))
        {
            Generate();
        }
        else
        {
            idDatabase.Add(_id, this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //if the objects already remove from the world, remove it from the database
        if (idDatabase.ContainsKey(_id))
        {
            idDatabase.Remove(_id);
        }
    }


    private void Generate()
    {
        _id = Guid.NewGuid().ToString();
        idDatabase.Add(_id, this.gameObject);
    }


}
