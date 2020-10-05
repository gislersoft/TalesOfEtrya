using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Animator animator;
    public int attackHash;
    private int attackModeHash;
    public int attackCount;
    public bool isAttacking;
    private const int maxAttacks = 3;

    [Header("Damage check")]
    public Transform handTransform;
    public Vector3 handOffset;
    public float radius;
    [SerializeField]
    public LayerMask layerMask;
    [HideInInspector]
    public bool canDealDamage;

    [Header("Stats")]
    public CharacterStats agentStats;
    public CharacterStats targetStats;
    public FX_Manager fxManager;

    [Header("Audio")]
    public TFOG_AudioManager_Johnny audioManager;
	// Use this for initialization
	void Start () {
        attackHash = Animator.StringToHash("PlayerAttack");
        attackModeHash = Animator.StringToHash("PlayerAttackCount");
        isAttacking = false;
        animator.SetBool(attackHash, isAttacking);
        attackCount = 0;
        canDealDamage = true;
        fxManager = FX_Manager.Instance;
    }

    private void Update()
    {
        if (TFOG_GameManager.Instance.isGameOver)
            return;
        if (isAttacking)
        {
            Collider2D blockTest = Physics2D.OverlapCircle(handTransform.position + handOffset, radius, layerMask, -Mathf.Infinity);

            if (blockTest != null)
            {
                if (blockTest.gameObject.tag == StringsInGame.EnemyTag && canDealDamage)
                {
                    targetStats.TakeDamage(agentStats.damage.GetValue());
                    canDealDamage = false;
                    if(fxManager != null)
                    {
                        fxManager.Shake();
                    }
                    if(audioManager != null)
                    {
                        audioManager.Hit();
                    }
                }
            }
        }
    }

    public void Attack()
    {
        if (TFOG_GameManager.Instance.isGameOver)
            return;
        if (isAttacking == false)
        {
            isAttacking = true;
            animator.SetBool(attackHash, isAttacking);

            attackCount = (attackCount + 1) % maxAttacks;
            animator.SetFloat(attackModeHash, attackCount);

            if(audioManager != null)
            {
                audioManager.Attack();
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(handTransform.position + handOffset, radius);
    }
}
