using UnityEngine;

public class MeleeAttack : MonoBehaviour {

    public static int CurrentAttack { get; set; }
    public static bool AttackRequestBlock = false;
    private const float timeToReset = 2f;
    private float resetCooldown;

    private Animator animator;
    public static int AttackModeHash { get; private set; }
    public static int IsAttackingHash;
    public static bool IsAttacking;

    private void Start()
    {
        animator = GetComponent<Animator>();
        IsAttackingHash = Animator.StringToHash("playerIsAttacking");
        AttackModeHash = Animator.StringToHash("playerAttackMode");
        CurrentAttack = 0;
    }


    public void Attack()
    {
        if(AttackRequestBlock == false)
        {
            animator.SetBool(IsAttackingHash, true);
            IsAttacking = true;
            AttackRequestBlock = true;
        }
    }
}
