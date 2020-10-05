using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up item " + item.name);
        bool wasPickesUp = Inventory.instance.Add(item);
        if (wasPickesUp)
        {
            RespawnInteractable respawn = gameObject.GetComponent<RespawnInteractable>();
            if(respawn != null)
            {
                respawn.Inactivate();
            }
            else
            {
                Destroy(gameObject);
            }
        }
            
    }
}
