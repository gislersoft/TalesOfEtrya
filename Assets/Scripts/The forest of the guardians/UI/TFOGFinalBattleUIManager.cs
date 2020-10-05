using UnityEngine.UI;
using UnityEngine;

public class TFOGFinalBattleUIManager : BaseUIManager {

    public static TFOGFinalBattleUIManager Instance;

    [Header("Aradis Health bar")]
    public Image aradisHealthBar;


    private void Awake()
    {
        Instance = this;
    }

    public void UpdateAradisHealth(int currentHealth, int maxHealth)
    {
        aradisHealthBar.fillAmount = ((float)currentHealth / (float)maxHealth);
    }
}
