using UnityEngine;

public class FSM_BaseAttack : FSM_Base {

    //public LongRangeCast castSpell;
    //public FaceTowardEnemy facing;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        //castSpell = animator.GetComponent<LongRangeCast>();

        attackCount = maxNumberOfAttacks;        

        //animator.SetFloat(baseAttackPhaseHash, currentAttack);
        //facing = agentTransform.GetComponent<FaceTowardEnemy>();

        //if (facing != null)
        //{
        //    facing.blockFacing = true;
        //}
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(TFOG_GameManager.Instance.isGameOver)
            return;
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        attackCooldown -= Time.deltaTime;

        if (attackCooldown < 0f && attackCount > 0)
        {
            if(castSpell != null)
            {
                castSpell.Spawn(agentTransform.position + new Vector3(0, 1, 0));
            }
            else
            {
                castSpell = animator.GetComponent<LongRangeCast>();
                if (castSpell != null)
                {
                    castSpell.Spawn(agentTransform.position + new Vector3(0, 1, 0));
                }
            }
            attackCooldown = 1f / attackSpeed;
            attackCount--;
        }

        if(attackCount <= 0)
        {
            //Move to MoveToPlayer state
            animator.SetTrigger(moveToPlayerTriggerHash);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
        //currentAttack = (currentAttack + 1) % 3;
        //facing = agentTransform.GetComponent<FaceTowardEnemy>();

        //if (facing != null)
        //{
        //    facing.blockFacing = false;
        //}
    }
}
