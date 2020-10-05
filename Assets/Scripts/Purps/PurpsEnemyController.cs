using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PurpsEnemyCombat))]
public class PurpsEnemyController : EnemyController
{
    PurpsEnemyCombat purpsEnemyCombat;
    public Animator animator;

	// Use this for initialization
	new void Start () {
        base.Start();
        purpsEnemyCombat = GetComponent<PurpsEnemyCombat>();
	}

    public override IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= navMeshAgent.stoppingDistance)
        {
            purpsEnemyCombat.Attack(playerStats);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }  
    }
}
