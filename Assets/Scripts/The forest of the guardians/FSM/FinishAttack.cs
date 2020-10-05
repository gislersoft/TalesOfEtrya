using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAttack : StateMachineBehaviour {

    //public FightAgent fightAgent;
    public PlayerAttack playerAttack;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //fightAgent = animator.GetComponent<FightAgent>();
        playerAttack = animator.GetComponent<PlayerAttack>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if(fightAgent != null)
        //{
        //   fightAgent.isAttacking = false;
        //    fightAgent.canDealDamage = true;
        //}

        if(playerAttack != null)
        {
            playerAttack.canDealDamage = true;
            playerAttack.isAttacking = false;
            animator.SetBool(playerAttack.attackHash, false);
        }
    }

}
