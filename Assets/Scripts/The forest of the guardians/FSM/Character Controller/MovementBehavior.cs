using UnityEngine;
using UnityEngine.AI;

public class MovementBehavior : AradisController {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        agent.updateRotation = false;

        player = PlayerManager.instance.player.transform;
        aradis = animator.gameObject.transform;

        attackModeHash = Animator.StringToHash("Aradis Attack");
        attackTriggerHash = Animator.StringToHash("Aradis Attack Trigger");
        movementHash = Animator.StringToHash("Aradis Movement");
        strafeHash = Animator.StringToHash("Aradis Strafe");
        cooldownHash = Animator.StringToHash("Cooldown");
        distanceHash = Animator.StringToHash("Distance");
        tauntHash = Animator.StringToHash("Taunt");
        tauntTriggerHash = Animator.StringToHash("Taunt Trigger");
        superTauntTriggerHash = Animator.StringToHash("Super Taunt Trigger");
        isFastMovingHash = Animator.StringToHash("isFastMoving");

               
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(aradisDifficulty == AradisDifficulty.EASY)
        {
            if(animator.GetFloat(distanceHash) > meleeRangeCheckDistance)
            {
                agent.isStopped = true;
            }
        }
        else if(aradisDifficulty == AradisDifficulty.MEDIUM)
        {
            agent.SetDestination(player.position);
        }
        else if(aradisDifficulty == AradisDifficulty.HARD)
        {
            fastMoveRequest = true;
            if(fastMoveRequest == true)
            {
                FastMoveTowardsPlayer();
            }
            else
            {
                agent.SetDestination(player.position);
            }

            if(animator.GetFloat(distanceHash) < meleeRangeCheckDistance)
            {
                fastMoveRequest = false;
            }
        }

        animator.SetFloat(strafeHash, Mathf.Clamp(agent.velocity.x, -1f, 1f));
        animator.SetFloat(movementHash, -Mathf.Clamp(agent.velocity.z, -1f, 1f));
        animator.SetFloat(distanceHash, Vector3.Distance(player.position, aradis.position));
    }

    public void FastMoveTowardsPlayer()
    {
        agent.SetDestination(player.position);
        agent.speed = movementSpeed * speedBoost;
    }


}
