using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rarity", menuName = "Rarity")]
public class Rarity : ScriptableObject
{
    [SerializeField] private string WRarity;
    [SerializeField] private string MAttack;
    [SerializeField] private string RAttack;
    [SerializeField] private float DropChance;
}
