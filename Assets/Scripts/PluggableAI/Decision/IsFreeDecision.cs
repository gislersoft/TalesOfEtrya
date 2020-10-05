using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Is free")]
public class IsFreeDecision : _Decision
{
    public override bool Decide(StateController controller)
    {
        return IsTimeOver(controller);
    }

    private bool IsTimeOver(StateController controller)
    {
        controller.navMeshAgent.isStopped = true;
        //controller.attachedToTree = false;
        return controller.CheckIfCountdownElapsed(10f);
    }
}
