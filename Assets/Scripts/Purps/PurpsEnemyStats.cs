using System.Collections;
using UnityEngine;

public class PurpsEnemyStats : CharacterStats {

    Animator animator;
    public float lookRadius = 0.5f;
    public float sphereCastLookRadius = 1f;
    public float lookRange = 50f;
    public float attackRange = 2f;
    public float attackRate = 1f;
    public float searchingTurnSpeed = 1f;
    public float searchDuration = 1f;
    public float cooldown = 1f;
    protected float cooldownTimer;

    public delegate void OnEnemyKilled(int score);
    public static OnEnemyKilled onEnemyKilledCallback;

    void Start()
    {
        cooldownTimer = cooldown;
        animator = gameObject.GetComponent<Animator>();
    }

    public override void Die()
    {
        //base.Die();
        StartCoroutine(PlayDieAnimation());
        EnemyKilledScore();
    }

    public static void EnemyKilledScore()
    {
        if (onEnemyKilledCallback != null)
        {
            onEnemyKilledCallback.Invoke(100);
        }
    }

    IEnumerator PlayDieAnimation()
    {
        animator.SetBool("isAttacking", false);
        
        animator.SetBool("isDead", true);
        yield return null;
        Destroy(gameObject);
    }
}
