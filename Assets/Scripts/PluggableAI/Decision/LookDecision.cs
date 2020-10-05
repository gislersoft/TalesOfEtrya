using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : _Decision
{
    public override bool Decide(StateController controller)
    {
        bool isTargetVisible = Look(controller);

        return isTargetVisible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit hit;

        //Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);

        if(Physics.SphereCast(
            controller.eyes.position, 
            controller.enemyStats.sphereCastLookRadius, 
            controller.eyes.forward, 
            out hit,
            controller.enemyStats.lookRange) 
            && hit.collider.tag == "Player")
        {
            controller.chaseTarget = hit.transform;
            return true;
        }

        return false;
    }
}
