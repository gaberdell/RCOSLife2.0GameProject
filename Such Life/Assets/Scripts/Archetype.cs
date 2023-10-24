using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Archetype", menuName = "Archetype")]
public class Archetype : ScriptableObject
{
    private string[] Archetypes = {"Heavy","Medium","Light"};
    private int[] WeaponDurabilities = {200,150,100};
    private float[] MoveSpeeds = {0,0,0};
    [SerializeField] private string Type;
    [SerializeField] private int Durability;
    [SerializeField] private float AttackMovesSpeed;
    /*
    public Archetype()
    {
    }
    public Archetype(int WeaponType)
    {
        Type = Archetypes[WeaponType];
        Durability = WeaponDurabilities[WeaponType];
        AttackMovesSpeed = MoveSpeeds[WeaponType];
    }
    */
}



