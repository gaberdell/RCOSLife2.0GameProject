using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{

    public float maxHP = 100;
    private float currHP;

    public healthBarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        healthBar.SetMaxHealth(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void decHP(float decAM){
        currHP -= decAM;

        healthBar.SetHealth(currHP);
    }
}
