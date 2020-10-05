using UnityEngine;

public class FSM_Attack : AradisFSM {

    [Header("Attack")]
    public const float meleeRangeCheckDistance = 1f;
    public const float maxCooldown = 1f;
    public float cooldown;

    public AttackCheck attackCheck;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        attackTriggerHash = Animator.StringToHash("AgentAttack");
        cooldown = maxCooldown;
        attackCheck = animator.GetComponent<AttackCheck>();
        attackCheck.isAttacking = false;
        attackCheck.canDealDamage = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cooldown -= Time.deltaTime;

        float distance = Mathf.Abs(targetTransform.localPosition.x - agentTransform.localPosition.x);

        if (distance < meleeRangeCheckDistance && cooldown < 0f && FSM_Taunt.isTaunting == false)
        {
            cooldown = maxCooldown;
            animator.SetTrigger(attackTriggerHash);
            attackCheck.isAttacking = true;
            FSM_Taunt.lastTimeAttacked = Time.timeSinceLevelLoad;
        }

        if(attackCheck != null)
        {
            attackCheck.Check(StringsInGame.PlayerTag);
        }
    }
}
