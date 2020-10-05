using UnityEngine;
public class AradisStats : CharacterStats {

    public Stat castDamage;
    private Animator animator;
    private int takeDamageHash;
    private int winTriggerHash;
    private int loseTriggerHash;
    
    
    void Start()
    {
        //ConsumableManager.instance.onConsumableUsedCallback += OnItemConsumed;
        animator = GetComponent<Animator>();
        takeDamageHash = Animator.StringToHash("AradisTakeDamage");
        winTriggerHash = Animator.StringToHash("AradisWin");
        loseTriggerHash = Animator.StringToHash("AradisLose");

        CurrentHealth = maxHealth;
    }

    public void DieAnimation(){
        animator.SetTrigger(loseTriggerHash);

    }

    public void WinAnimation(){
        animator.SetTrigger(winTriggerHash);

    }

    public override void Die(){
        base.Die();
        if(TFOG_GameManager.Instance != null){
            TFOG_GameManager.Instance.EndGame();
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        TFOGFinalBattleUIManager.Instance.UpdateAradisHealth(CurrentHealth, maxHealth);
        animator.SetTrigger(takeDamageHash);
    }
}
