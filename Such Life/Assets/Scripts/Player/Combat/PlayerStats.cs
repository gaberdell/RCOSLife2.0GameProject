using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Base values for Player
    public int BaseAtk = 10;
    //public int BaseDef = 10;
    public int BaseSpd = 2;
    public int BaseHP = 100;
    public int BaseDef = 50;
    public int BaseCritRate = 0;
    public int BaseCritDmg = 0;
    
    // Player Stats (multipliers for other scripts)
    public float PlayerAtk = 1.0f;
    public float PlayerSpd = 1.0f;
    public float PlayerHP = 1.0f;
    public float PlayerDef = 1.0f;
    public float PlayerCritRate = 0.5f;
    public float PlayerCritDmg = 1.5f;
}
