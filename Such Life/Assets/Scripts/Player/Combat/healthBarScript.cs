using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarScript : MonoBehaviour
{
    public Slider slider;

    private void OnEnable() {
        EventManager.setPlayerHealthBar += SetHealth;
    }

    private void OnDisable() {
        EventManager.setPlayerHealthBar -= SetHealth;
    }

    public void SetMaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health){
        slider.value = health;
    }
}
