using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{

    [SerializeField] private float maxHP = 100;
    private float currHP;

    private void OnEnable()
    {
        EventManager.onDealDamage += OnDealtDamage;
    }

    private void OnDisable()
    {
        EventManager.onDealDamage -= OnDealtDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        EventManager.SetPlayerHealthBar(maxHP);
    }

    // Unity Quick Tip if update isn't needed Remove it, leaving it slows down performance
    private bool OnDealtDamage(GameObject isOurObject, float damageAmount)
    {
        if (isOurObject)
        {
            decHP(damageAmount);
            return true;
        }
        return false;
    }

    private void decHP(float decAM)
    {    
        currHP -= decAM;
        EventManager.SetPlayerHealthBar(currHP);
    }
}
