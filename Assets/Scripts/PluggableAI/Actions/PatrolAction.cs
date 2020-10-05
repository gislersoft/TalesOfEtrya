using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : ActionSO
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    void Patrol(StateController controller)
    {
        controller.navMeshAgent.destination = controller.waypointList[controller.nextWayPoint].position;
        controller.navMeshAgent.isStopped = false;
        if(
            controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance &&
            !controller.navMeshAgent.pathPending
            )
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.waypointList.Count;
        }
    }
}
