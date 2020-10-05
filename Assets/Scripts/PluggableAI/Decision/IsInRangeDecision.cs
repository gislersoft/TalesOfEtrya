using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/In range")]
public class IsInRangeDecision : _Decision
{
    public override bool Decide(StateController controller)
    {
        bool isTargetInRange = Look(controller);

        return isTargetInRange;
    }

    private bool Look(StateController controller)
    {
        return 
            (controller.enemyStats.lookRange > 
            (controller.transform.position - controller.chaseTarget.position).magnitude) ?
            true : false;
    }

}
