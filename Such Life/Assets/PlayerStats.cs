using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Base values for player
    public int baseAtk = 10;
    public int baseDef = 10;
    public int baseSpd = 2;
    public int baseHP = 100;
    public int baseDef = 50;
    public int baseCritRate = 0;
    public int baseCritDmg = 0;
    
    // Player Stats (multipliers for other scripts)
    public float playerAtk = 1.0f;
    public float playerDef = 1.0f;
    public float playerSpd = 1.0f;
    public float playerHP = 1.0f;
    public float playerDef = 1.0f;
    public float playerCritRate = 0.5f;
    public float playerCritDmg = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
