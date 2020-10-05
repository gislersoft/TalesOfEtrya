using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
    [SerializeField]
    private int baseValue;
    private List<int> consumables = new List<int>();

    public void SetValue(int value)
    {
        baseValue = value;
    }

    public int GetBaseValue() {
        return baseValue;
    }

    public int GetValue()
    {
        int finalValue = baseValue;
        consumables.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddWeaponModifier(int modifier)
    {
        baseValue = modifier;
    }

    public void AddConsumableEffect(int modifier)
    {
        if(modifier != 0)
        {
            consumables.Add(modifier);
        }
    }

    public void RemoveConsumableEffect(int modifier)
    {
        if(modifier != 0)
        {
            consumables.Remove(modifier);
        }
    }
}
