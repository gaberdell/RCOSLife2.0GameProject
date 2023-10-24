using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Archetype", menuName = "Archetype")]
public class Archetype : ScriptableObject
{
    [SerializeField] private string Type;
    [SerializeField] private int Durability;
    [SerializeField] private float AttackMovesSpeed;
}



