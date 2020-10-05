using EZCameraShake;
using UnityEngine;

public class SuperTaunt : AttackBehavior {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CameraShaker.Instance.ShakeOnce(10, 5, 2, 3);

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        cooldown -= Time.deltaTime;
        animator.SetFloat(cooldownHash, cooldown);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        isSuperTaunting = false;

        if (isFastMoving)
        {
            isFastMoving = false;
            animator.SetBool(isFastMovingHash, false);
            fastMoveRequest = true;
        }

        if (isComboing == false)
        {
            cooldown = maxCooldown;
            animator.SetFloat(cooldownHash, cooldown);
        }
    }
}
