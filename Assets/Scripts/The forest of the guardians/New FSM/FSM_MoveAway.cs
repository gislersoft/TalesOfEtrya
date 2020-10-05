using UnityEngine;

public class FSM_MoveAway : FSM_Base {
    Vector3 direction = Vector3.zero;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateEnter(animator, stateInfo, layerIndex);

        float playerDistanceToLeft = Mathf.Abs(targetTransform.localPosition.x - leftCornerPosition.x);
        float playerDistanceToRight = Mathf.Abs(targetTransform.localPosition.x - rightCornerPosition.x);

        if (playerDistanceToLeft > playerDistanceToRight)
        {
            direction.x = -moveSpeed;
            direction.y = direction.z = 0;
        }
        else
        {
            direction.x = moveSpeed;
            direction.y = direction.z = 0;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(TFOG_GameManager.Instance.isGameOver)
            return;
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);
        agentTransform.localPosition = Vector3.Lerp(agentTransform.localPosition, agentTransform.localPosition + direction, Time.deltaTime);

        float distanceToRight = agentTransform.localPosition.x - rightCornerPosition.x;
        float distanceToLeft = agentTransform.localPosition.x - leftCornerPosition.x;
        if ((-1f < distanceToRight && distanceToRight < 1f) ||
            (-1 < distanceToLeft && distanceToLeft < 1f))
        {
            animator.SetTrigger(baseAttackTriggerHash);
        }

        float distanceToTarget = Mathf.Abs(targetTransform.localPosition.x - agentTransform.localPosition.x);
        if (distanceToTarget < meleeRange)
        {
            animator.SetTrigger(meleeAttackTriggerHash);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
