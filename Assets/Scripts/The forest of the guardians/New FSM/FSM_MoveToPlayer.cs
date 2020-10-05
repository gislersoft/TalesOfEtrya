using UnityEngine;

public class FSM_MoveToPlayer : FSM_Base {

    Vector3 direction;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        direction = Vector3.zero;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(TFOG_GameManager.Instance.isGameOver)
            return;
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);

        if (Mathf.Abs(agentTransform.localPosition.x - targetTransform.localPosition.x) < meleeRange)
        {
            //Move to melee attack state
            animator.SetTrigger(meleeAttackTriggerHash);
        }else
        {
            direction.y = 0;
            direction.z = 0;
            direction.x = (targetTransform.localPosition.x - agentTransform.localPosition.x > 0) ? moveSpeed : -moveSpeed;

            agentTransform.localPosition = Vector3.Lerp(agentTransform.localPosition, agentTransform.localPosition + direction, Time.deltaTime);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
