using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        //to be overwritten
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        //get inventory info and remove that specific item

    }

}
