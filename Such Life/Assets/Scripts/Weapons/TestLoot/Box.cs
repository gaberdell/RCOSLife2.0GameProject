using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Box : MonoBehaviour
{   
    public GameObject gameObject;
    void Destroy(GameObject gameObject)
    {
        GameObject.Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        GetComponent<WeaponBag>().InstantiateWeapon(transform.position);
        Destroy(gameObject);
    }
}
