using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FStat{

    [SerializeField]
    private float baseValue;
    private List<float> consumables = new List<float>();

    public float GetBaseValue()
    {
        return baseValue;
    }

    public float GetValue()
    {
        float finalValue = baseValue;
        consumables.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddWeaponModifier(float modifier)
    {
        baseValue = modifier;
    }

    public void AddConsumableEffect(float modifier)
    {
        if (modifier != 0)
        {
            consumables.Add(modifier);
        }
    }

    public void RemoveConsumableEffect(float modifier)
    {
        if (modifier != 0)
        {
            consumables.Remove(modifier);
        }
    }
}
