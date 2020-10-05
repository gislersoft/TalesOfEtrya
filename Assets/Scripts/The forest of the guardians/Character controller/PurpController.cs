using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpController : EnemyController
{
    private Animator animator;
    TFOG_CharacterMovement playerMovement;
    private int purpAttackHash;
    private float distance;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        playerMovement = PlayerManager.instance.player.GetComponent<TFOG_CharacterMovement>();
        purpAttackHash = Animator.StringToHash("purpAttack");
    }

    public override IEnumerator Attack()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool(purpAttackHash, true);

        yield return new WaitForSeconds( animator.GetCurrentAnimatorClipInfo(0)[0].clip.length );

        distance = (transform.position - target.position).magnitude;
        
        if (distance < navMeshAgent.stoppingDistance)
        {
            DealDamage();
        }

        animator.SetBool(purpAttackHash, false);
        navMeshAgent.isStopped = false;
    }

    public void DealDamage()
    {
        enemyCombat.Attack(playerStats);
        playerMovement.GetHitAnimation();
    }
}
