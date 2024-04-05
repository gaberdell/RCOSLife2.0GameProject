using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Archetype", menuName = "Archetype")]
public class Archetype : ScriptableObject
{
    [SerializeField] private string _type;
    [SerializeField] private int _durability;
    [SerializeField] private float _attackMovesSpeed;
}



