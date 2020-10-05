using UnityEngine;

public class ChangeAttack : AttackBehavior {

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (isCastingSpell)
        {
            castEffect.Spawn(player.position);
        }
        else
        {
            currentAttack = (currentAttack + 1) % 3;
            animator.SetFloat(attackModeHash, currentAttack);
        }

        if (!isComboing)
        {
            cooldown = maxCooldown;
            animator.SetFloat(cooldownHash, cooldown);
        }
        isCastingSpell = false;
    }
}
