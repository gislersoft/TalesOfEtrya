using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterMovement))]
public class EnemyController : MonoBehaviour {

    [HideInInspector, SerializeField]
    public Transform target;
    [HideInInspector, SerializeField]
    protected CharacterStats playerStats;
    [HideInInspector, SerializeField]
    public NavMeshAgent navMeshAgent;
    [HideInInspector, SerializeField]
    public CharacterMovement characterMovement;
    [HideInInspector, SerializeField]
    protected CharacterCombat enemyCombat;

    public float lookRadius = 10f;
    public float smoothRotate = 7f;
    public float stoppingDistanceOffset = 0f;

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        target = PlayerManager.instance.player.transform;
        playerStats = target.GetComponent<PlayerStats>();
        enemyCombat = GetComponent<CharacterCombat>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {

        float distance = Vector3.Distance(transform.position, target.position);

        if (navMeshAgent.enabled)
        {
            if (distance <= lookRadius)
            {
                navMeshAgent.SetDestination(target.position);
                navMeshAgent.isStopped = false;
                characterMovement.Move(navMeshAgent.velocity, false);
                if (distance <= (navMeshAgent.stoppingDistance + stoppingDistanceOffset))
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                navMeshAgent.isStopped = true;
            }
        }
        FaceTarget();

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(
            new Vector3(direction.x, 0, direction.z)
        );

        Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, lookRotation, smoothRotate * Time.deltaTime);
        transform.rotation = smoothRotation;
    }

    public virtual IEnumerator Attack()
    {
        //Use this method for the attacking animation of your character
        //Remember to stop your model using navMeshAgent.isStopped = true;
        //You can find and example in ZombieEnemyController.cs
        yield return null;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        navMeshAgent = GetComponent<NavMeshAgent>();
        Gizmos.DrawWireSphere(transform.position, navMeshAgent.stoppingDistance);
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
