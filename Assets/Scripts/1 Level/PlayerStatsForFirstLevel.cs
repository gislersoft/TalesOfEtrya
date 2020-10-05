using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;

public class PlayerStatsForFirstLevel : CharacterStats {

    #region Singleton
    public static PlayerStatsForFirstLevel instance;
    private static PlayerStatsForFirstLevel playerStats;
    private Animator animator;
    private int takeDamageHash;
    InGameUIManagerTimerClue uiManager;


    private void Awake() {
        instance = this;
    }

    #endregion
    // Use this for initialization

    public static PlayerStatsForFirstLevel Instance() {
        if (!playerStats) {
            playerStats = FindObjectOfType( typeof( PlayerStatsForFirstLevel ) ) as PlayerStatsForFirstLevel;
            if (!playerStats)
                Debug.LogError( "There needs to be one active GetPlayerPosition script on a GameObject in your scene." );
        }

        return playerStats;
    }

    void Start() {
        //ConsumableManager.instance.onConsumableUsedCallback += OnItemConsumed;
        animator = GetComponent<Animator>();
        takeDamageHash = Animator.StringToHash( "getHit" );
        uiManager = InGameUIManagerTimerClue.instance;
        base.CurrentHealth = maxHealth;
    }


    void OnItemConsumed(Consumable consumable, float timeElapsed) {
        if (consumable != null) {
            if (timeElapsed >= 0f) {
                armor.AddConsumableEffect( consumable.armorModifier );
                damage.AddConsumableEffect( consumable.damageModifier );
            } else {
                armor.RemoveConsumableEffect( consumable.armorModifier );
                damage.RemoveConsumableEffect( consumable.damageModifier );
            }

        }
    }

    public override void Die() {
        //base.Die();
        print( "Character died" );
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage( damage );
        uiManager.UpdateHealth( CurrentHealth + 0f, maxHealth + 0f );
        animator.SetTrigger( takeDamageHash );
    }
}
