using UnityEngine;

public class FSM_MeleeAttack : FSM_Base {
    //protected AttackCheck attackCheck;
    //public TFOG_AudioManager_Johnny audioManager;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateEnter(animator, stateInfo, layerIndex);
        //attackCheck = animator.GetComponent<AttackCheck>();
        //audioManager = TFOGPlayerManager.instance.Aradis.GetComponent<TFOG_AudioManager_Johnny>();
        //facing = agentTransform.GetComponent<FaceTowardEnemy>();

        if(facing != null)
        {
            facing.blockFacing = true;
        }

        if(audioManager != null)
        {
            audioManager.Attack();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(TFOG_GameManager.Instance.isGameOver)
            return;
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);

        if (attackCheck != null)
        {
            attackCheck.isAttacking = true;
            if (attackCheck.Hit(StringsInGame.PlayerTag))
            {
                animator.SetTrigger(moveAwayTriggerHash);
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
        if (attackCheck != null)
        {
            attackCheck.isAttacking = false;
            attackCheck.canDealDamage = true;
        }
        currentAttack = (currentAttack + 1) % 3;
        animator.SetFloat(baseAttackPhaseHash, currentAttack);
        if (facing != null)
        {
            facing.blockFacing = false;
        }
    }
}
