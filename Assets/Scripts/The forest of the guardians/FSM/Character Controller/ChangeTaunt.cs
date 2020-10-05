using UnityEngine;
public class ChangeTaunt : AttackBehavior {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        cooldown -= Time.deltaTime;
        animator.SetFloat(cooldownHash, cooldown);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        currentTaunt = Random.Range(0, 2);

        isTaunting = false;

        cooldown = maxCooldown;
        animator.SetFloat(cooldownHash, cooldown);
    }
}
