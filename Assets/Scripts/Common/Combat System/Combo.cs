using UnityEngine;

public class Combo : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        MeleeAttack.AttackRequestBlock = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        MeleeAttack.AttackRequestBlock = false;

        //MeleeAttack.CurrentAttack = (MeleeAttack.CurrentAttack + 1) % 3;
        //animator.SetFloat(MeleeAttack.AttackModeHash, MeleeAttack.CurrentAttack);

        MeleeAttack.IsAttacking = false;
        animator.SetBool(MeleeAttack.IsAttackingHash, false);
    }
}
