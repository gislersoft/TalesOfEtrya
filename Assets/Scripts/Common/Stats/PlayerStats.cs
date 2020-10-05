using UnityEngine;

public class PlayerStats : CharacterStats {

    #region Singleton
    public static PlayerStats instance;
    private static PlayerStats playerStats;
    private Animator animator;
    private int takeDamageHash;
    private int loseHash;
    private int winTriggerHash;
    BaseUIManager uiManager;


    private void Awake() {
        instance = this;
    }

    #endregion
    // Use this for initialization

    public AudioSource getHitAudio;

    public static PlayerStats Instance() {
        if (!playerStats) {
            playerStats = FindObjectOfType( typeof( PlayerStats ) ) as PlayerStats;
            if (!playerStats)
                Debug.LogError( "There needs to be one active GetPlayerPosition script on a GameObject in your scene." );
        }

        return playerStats;
    }

    void Start()
    {
        //ConsumableManager.instance.onConsumableUsedCallback += OnItemConsumed;
        animator = GetComponent<Animator>();
        takeDamageHash = Animator.StringToHash("getHit");
        loseHash = Animator.StringToHash("lose");
        uiManager = FindObjectOfType<BaseUIManager>();
        CurrentHealth = maxHealth;
    }


    void OnItemConsumed(Consumable consumable, float timeElapsed)
    {
        if(consumable != null)
        {
            if(timeElapsed >= 0f)
            {
                armor.AddConsumableEffect(consumable.armorModifier);
                damage.AddConsumableEffect(consumable.damageModifier);
            }
            else
            {
                armor.RemoveConsumableEffect(consumable.armorModifier);
                damage.RemoveConsumableEffect(consumable.damageModifier);
            }
            
        }
    }
    public void DieAnimation(){
        animator.SetTrigger(loseHash);

    }

    public void WinAnimation(){
        // animator.SetTrigger(winTriggerHash);
        Debug.Log("Player won anim");
    }
    public override void Die()
    {
        base.Die();
        if(TFGGameManager.instance != null)
        {
            TFGGameManager.instance.LoseTheGame();
            animator.SetTrigger(loseHash);
        }

        if(TFOG_GameManager.Instance != null){
            TFOG_GameManager.Instance.EndGame();
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        uiManager.UpdateHealth(CurrentHealth + 0f, maxHealth + 0f);
        animator.SetTrigger(takeDamageHash);
        if(getHitAudio != null)
        {
            getHitAudio.Play();
        }
    }
}
