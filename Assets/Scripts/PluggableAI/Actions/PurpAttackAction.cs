using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Purp attack")]
public class PurpAttackAction : ActionSO {
    
    public override void Act(StateController controller)
    {
        Type controllerType = controller.GetType();
        var animatorInfo = controllerType.GetField("animator");

        var purpsAnimator = (Animator)animatorInfo.GetValue(controller);

        
        Attack(controller, purpsAnimator);
    }

    void Attack(StateController controller, Animator purpsAnimator)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);

        if (Physics.SphereCast(
            controller.eyes.position,
            controller.enemyStats.sphereCastLookRadius,
            controller.eyes.forward,
            out hit,
            controller.enemyStats.attackRange)
            && hit.collider.tag == "Player")
        {
            if (controller.CheckIfCountdownElapsed(controller.enemyStats.attackRate))
            {
                purpsAnimator.SetBool("isAttacking", true);
            }
            
        }
        else
        {
            purpsAnimator.SetBool("isAttacking", false);
        }
    }



}
