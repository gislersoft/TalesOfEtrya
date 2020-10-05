using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableManager : MonoBehaviour {

    #region Singleton
    public static ConsumableManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnConsumableUsed(Consumable consumable, float timeElapsed);
    public OnConsumableUsed onConsumableUsedCallback;

    [SerializeField]
    List<Consumable> currentConsumables = new List<Consumable>();
    [SerializeField]
    List<float> consumableTime = new List<float>();

    public void Consume(Consumable consumable)
    {
        currentConsumables.Add(consumable);
        consumableTime.Add(consumable.totalDuration);

        if(onConsumableUsedCallback != null)
        {
            onConsumableUsedCallback.Invoke(consumable, consumable.totalDuration);
        }
    }

    public void RemoveEffectOfConsumable(int consumablePos)
    {
        Consumable consumed = currentConsumables[consumablePos];
        float timeElapsed = consumableTime[consumablePos];
        currentConsumables.RemoveAt(consumablePos);
        consumableTime.RemoveAt(consumablePos);

        if (onConsumableUsedCallback != null)
        {
            onConsumableUsedCallback.Invoke(consumed, timeElapsed);
        }
    }

    void Update()
    {
        if (currentConsumables.Count != consumableTime.Count)
        {
            Debug.LogWarning("Error at the size of the consumables list. More items on one of the arrays");
            return;
        }
        for (int i = 0; i < consumableTime.Count; i++)
        {
            consumableTime[i] -= Time.deltaTime;

            if (consumableTime[i] <= 0f)
                RemoveEffectOfConsumable(i);
        }

    }
}
