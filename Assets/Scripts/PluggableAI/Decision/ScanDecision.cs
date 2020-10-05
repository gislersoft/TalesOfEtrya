using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Scan")]
public class ScanDecision : _Decision
{
    public override bool Decide(StateController controller)
    {
        bool noEnemyInSight = Scan(controller);

        return noEnemyInSight;
    }

    private bool Scan(StateController controller)
    {
        controller.navMeshAgent.isStopped = true;

        controller.transform.Rotate(0f, controller.enemyStats.searchingTurnSpeed * Time.deltaTime, 0f);

        return controller.CheckIfCountdownElapsed(controller.enemyStats.searchDuration);
    }
}
