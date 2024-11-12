using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* This is an workaround way of trying to serialize dictionary. Unity does not allow serializing dictionary */
/* Thank you for the people who answer the question in the post: https://answers.unity.com/questions/460727/how-to-serialize-dictionary-with-unity-serializati.html */
//Old version is bricked shout out to Eduard Malkhasyan for fixing this old thing. This code is based off of his
//Find his repo here https://github.com/EduardMalkhasyan/Serializable-Dictionary-Unity/tree/master
//Lastly https://www.youtube.com/watch?v=hG3q-RMD0eA Ian McManus video here very helpful

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    void ISerializationCallbackReceiver.OnBeforeSerialize() { }

#if UNITY_EDITOR
    public void EditorOnly_Add(TKey addKey, TValue addValue)
    {
        keys.Add(addKey);
        values.Add(addValue);
    }
#endif

    // load dictionary from lists
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        if (keys.Count == values.Count)
        {
            this.Clear();
            for (int i = 0; i < keys.Count; i++)
                this[keys[i]] = values[i];
        }
    }
}
