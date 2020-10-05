using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterMovement))]
public class PurpAI : StateController {

    public List<Transform> purpWayPointList;

    [HideInInspector]
    public CharacterMovement purpsMovement;
    [HideInInspector]
    public Animator animator;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        animator = GetComponent<Animator>();
        chaseTarget = PlayerManager.instance.player.GetComponent<Transform>();
        purpsMovement = GetComponent<CharacterMovement>();
        SetupAI(true, purpWayPointList);
    }


    private void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookRadius);
        }
    }
}
