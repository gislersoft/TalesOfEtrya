using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StateController : MonoBehaviour {

   
    public Transform eyes;
    public State currentState;
    public PurpsEnemyStats enemyStats;
    public State remainState;

    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public List<Transform> waypointList;
    [HideInInspector]
    public int nextWayPoint;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public float stateTimeElapsed;
    
    protected bool isAiActive;
    

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        chaseTarget = PlayerManager.instance.player.GetComponent<Transform>();
    }

    public void SetupAI(bool aiActivation, List<Transform> waypointListFromManager)
    {
        waypointList = waypointListFromManager;
        isAiActive = aiActivation;

        if (isAiActive)
        {
            navMeshAgent.enabled = true;
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }

    protected void Update()
    {
        if (!isAiActive)
            return;
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountdownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0f;
    }

    private void OnDrawGizmos()
    {
        if(currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookRadius);
        }
    }
}
