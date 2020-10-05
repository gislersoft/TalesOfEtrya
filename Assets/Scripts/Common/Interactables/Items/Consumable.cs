using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item {

    public int armorModifier;
    public int damageModifier;
    public float totalDuration;

    public override void Use()
    {
        base.Use();
        ConsumableManager.instance.Consume(this);
        RemoveFromInventory();
    }
}

