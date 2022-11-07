using System;
using System.Collections;
using UnityEngine;

/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/


[System.Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField] private string _id = Guid.NewGuid().ToString();
}
