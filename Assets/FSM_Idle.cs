using UnityEngine;

public class FSM_Idle : FSM_Base {
    //This method initializes every component used in the state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //FSM Base
        agentTransform = animator.GetComponent<Transform>();
        targetTransform = TFOGPlayerManager.instance.player.transform;

        moveAwayTriggerHash = Animator.StringToHash(StringsInGame.MoveAwayTriggerHashName);
        baseAttackTriggerHash = Animator.StringToHash(StringsInGame.BaseAttackTriggerHashName);
        meleeAttackTriggerHash = Animator.StringToHash(StringsInGame.MeleeAttackTriggerHash);
        moveToPlayerTriggerHash = Animator.StringToHash(StringsInGame.MoveToPlayerTriggerHash);
        baseAttackPhaseHash = Animator.StringToHash(StringsInGame.BaseAttackHash);
        attackSpeedHash = Animator.StringToHash(StringsInGame.AttackSpeedHash);

        agentStats = TFOGPlayerManager.instance.Aradis.GetComponent<AradisStats>();
        targetStats = TFOGPlayerManager.instance.player.GetComponent<PlayerStats>();

        if (!started)
        {
            aradisDifficulty = AradisDifficulty.MEDIUM;
            attackSpeed = difficultyParameters[aradisDifficulty].attackSpeed;
            moveSpeed = difficultyParameters[aradisDifficulty].moveSpeed;
            agentStats.armor.SetValue(difficultyParameters[aradisDifficulty].armor);
            evaluationCooldown = evaluationTime;

            started = true;
            animator.SetFloat(attackSpeedHash, attackSpeed);
        }
        //FSM Base attack
        castSpell = animator.GetComponent<LongRangeCast>();
        facing = agentTransform.GetComponent<FaceTowardEnemy>();

        //Melee attack
        attackCheck = animator.GetComponent<AttackCheck>();
        audioManager = TFOGPlayerManager.instance.Aradis.GetComponent<TFOG_AudioManager_Johnny>();
    }
}
