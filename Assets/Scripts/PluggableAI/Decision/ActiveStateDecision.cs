using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : _Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.chaseTarget.gameObject.activeSelf;
    }
}
