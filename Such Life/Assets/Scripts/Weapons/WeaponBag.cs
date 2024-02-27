using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBag : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public List<Weapon> WeaponList = new List<Weapon>();
    Weapon GetDroppedWeapon()
    {
        List<Weapon> PossibleWeapons = new List<Weapon>();
        foreach(Weapon Item in WeaponList)
        {
            Weapon newItem = ScriptableObject.Instantiate(Item);
            newItem.constructor();
            PossibleWeapons.Add(newItem);
        }
        if(PossibleWeapons.Count > 0)
        {
            Weapon DropWeapon = PossibleWeapons[Random.Range(0,PossibleWeapons.Count)];
            return DropWeapon;
        }
        Debug.Log("no weapon dropped");
        return null;
    }

    public void InstantiateWeapon(Vector3 Position)
    {
        Weapon DroppedItem = GetDroppedWeapon();
        if(DroppedItem  != null)
        {
            GameObject LootGameObject = Instantiate(WeaponPrefab, Position, Quaternion.identity);
            LootGameObject.GetComponent<SpriteRenderer>().sprite = DroppedItem.sprite; //now we need a sprite for weapon drop
            LootGameObject.GetComponent<WeaponData>().AssignSO(DroppedItem);
        }
    }
}
