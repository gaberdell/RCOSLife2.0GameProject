using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBag : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public List<Weapon> WeaponList = new List<Weapon>();
    Weapon GetDroppedWeapon()
    {
        var possibleWeapons = new List<Weapon>();
        foreach(Weapon item in WeaponList){
            Weapon newItem = ScriptableObject.Instantiate(item);
            newItem.constructor();
            possibleWeapons.Add(newItem);
        }

        if(possibleWeapons.Count > 0){

            Weapon dropWeapon = possibleWeapons[Random.Range(0,possibleWeapons.Count)];
            return dropWeapon;
        }

        Debug.Log("no weapon dropped");
        return null;
    }

    public void InstantiateWeapon(Vector3 Position)
    {
        Weapon droppedItem = GetDroppedWeapon();

        if(droppedItem  != null){

            GameObject lootGameObject = Instantiate(WeaponPrefab, Position, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.sprite; //now we need a sprite for weapon drop
            lootGameObject.GetComponent<WeaponData>().AssignSO(droppedItem);
        }

    }
    
}
