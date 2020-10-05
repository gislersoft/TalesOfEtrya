using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : AradisController {

    [Header("Combat")]
    public static float meleeRange = 1f;
    public static LongRangeCast castEffect;
    public static bool isCastingSpell = false;
    
    public static bool isAttacking = false;
    public static bool useUltimate = false;
    public static float cooldown;
    public static bool isComboing = false;
    public static int currentAttack = 0;
    public static int currentTaunt = 0;
    public static bool isTaunting = false;
    public static bool isSuperTaunting = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cooldown = maxCooldown;
        animator.SetFloat(cooldownHash, cooldown);

        castEffect = animator.gameObject.GetComponent<LongRangeCast>();

        isAttacking = false;
        isComboing = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cooldown -= Time.deltaTime;
        animator.SetFloat(cooldownHash, cooldown);

        //if (cooldown > 0f)
        //    return;

        if(aradisDifficulty == AradisDifficulty.EASY)
        {
            EasyDifficulty(animator);
        }
        else if(aradisDifficulty == AradisDifficulty.MEDIUM)
        {
            MediumDifficulty(animator);
        }
        else if(aradisDifficulty == AradisDifficulty.HARD)
        {
            HardDifficulty(animator);
        }

        
    }

    public void MeleeAttack(Animator animator)
    {
        animator.SetFloat(attackModeHash, currentAttack);
        animator.SetTrigger(attackTriggerHash);
    }

    public void CastSpell(Animator animator)
    {
        animator.SetFloat(attackModeHash, 3);
        animator.SetTrigger(attackTriggerHash);
        isCastingSpell = true;
    }

    public void Taunt(Animator animator)
    {
        isTaunting = true;
        animator.SetFloat(tauntHash, currentTaunt);
        animator.SetTrigger(tauntTriggerHash);
    }

    public void SuperTaunt(Animator animator)
    {
        isSuperTaunting = true;
        animator.SetTrigger(superTauntTriggerHash);
    }
    
    public void EasyDifficulty(Animator animator)
    {
        animator.ResetTrigger(attackTriggerHash);
        animator.ResetTrigger(superTauntTriggerHash);
        animator.ResetTrigger(tauntTriggerHash);

        float distance = animator.GetFloat(distanceHash);
        if (distance < meleeRangeCheckDistance)
        {
            MeleeAttack(animator);
        }
        else if (distance > castRangeCheckDistance)
        {
            float rng = Random.Range(0f, 1f);
            if (rng > 0f && rng <= 0.5f && isCastingSpell == false && isSuperTaunting == false)
            {
                Taunt(animator);
            }
            else if(rng > 0.5f && rng <= 0.95f && isTaunting == false && isSuperTaunting == false)
            {
                CastSpell(animator);
            }
            else if(rng > 0.95f && isCastingSpell == false && isTaunting == false)
            {
                SuperTaunt(animator);
            }
        }

        if (distance > meleeRangeCheckDistance)
        {
            isComboing = false;
        }
    }

    public void MediumDifficulty(Animator animator)
    {
        animator.ResetTrigger(attackTriggerHash);
        animator.ResetTrigger(superTauntTriggerHash);
        animator.ResetTrigger(tauntTriggerHash);

        float distance = animator.GetFloat(distanceHash);
        if (distance < meleeRangeCheckDistance)
        {
            isComboing = true;
            MeleeAttack(animator);
        }
        else if (distance > castRangeCheckDistance)
        {
            CastSpell(animator);
        }

        if (distance > meleeRangeCheckDistance)
        {
            isComboing = false;
        }
    }

    public void HardDifficulty(Animator animator)
    {
        animator.ResetTrigger(attackTriggerHash);
        animator.ResetTrigger(superTauntTriggerHash);
        animator.ResetTrigger(tauntTriggerHash);

        if (fastMoveRequest)
        {
            return;
        }

        float distance = animator.GetFloat(distanceHash);
        if (distance <= meleeRangeCheckDistance)
        {
            isComboing = true;
            animator.SetBool(isFastMovingHash, false);
            MeleeAttack(animator);
        }
        else if (distance >= castRangeCheckDistance)
        {
            if(Random.Range(0f, 1f) > 0.6)
            {
                isComboing = true;
                isFastMoving = true;
                animator.SetBool(isFastMovingHash, true);

                CastSpell(animator);
                SuperTaunt(animator);
            }
            else
            {
                CastSpell(animator);
            }

            
        }

        if (distance > meleeRangeCheckDistance)
        {
            isComboing = false;
        }
    }
}
