using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEngine;

public class WeaponData : MonoBehaviour{   
    private Weapon _weaponSO; // scriptableobject
    public void AssignSO(Weapon weaponSO){
        _weaponSO = weaponSO;
    }
}

