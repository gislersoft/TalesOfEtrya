using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Reached destination")]
public class ReachedDestinationDecision : _Decision {

    public override bool Decide(StateController controller)
    {
        return ReachedDestination(controller);
    }

    private bool ReachedDestination(StateController controller)
    {
        float distance = controller.navMeshAgent.remainingDistance;

        return (distance < controller.navMeshAgent.stoppingDistance) ? true : false;
    }
}
