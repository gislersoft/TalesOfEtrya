using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "Item name";
    public Sprite icon = null;


    public virtual void Use()
    {
        //Use the item
        Debug.Log("Using item " + this.name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
