using UnityEngine;

public class AttackCheck : MonoBehaviour {

    [Header("Damage check")]
    public Transform handTransform;
    public Vector3 handOffset;
    public float radius;
    [SerializeField]
    public LayerMask layerMask;
    //[HideInInspector]
    public bool canDealDamage;
    //[HideInInspector]
    public bool isAttacking;

    [Header("Stats")]
    public CharacterStats agentStats;
    public CharacterStats targetStats;
    public FX_Manager fxManager;

    [Header("Audio")]
    public TFOG_AudioManager_Johnny audioManager;

    void Start () {
        canDealDamage = true;
        isAttacking = false;
        fxManager = FX_Manager.Instance;
    }

    public void Check(string tag)
    {
        if (isAttacking)
        {
            Collider2D blockTest = Physics2D.OverlapCircle(handTransform.position + handOffset, radius, layerMask, -Mathf.Infinity);

            if (blockTest != null)
            {
                if (blockTest.gameObject.tag == tag && canDealDamage)
                {
                    targetStats.TakeDamage(agentStats.damage.GetValue());
                    canDealDamage = false;
                    if (fxManager != null)
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

    public bool Hit(string tag)
    {
        if (isAttacking)
        {
            Collider2D blockTest = Physics2D.OverlapCircle(handTransform.position + handOffset, radius, layerMask, -Mathf.Infinity);

            if (blockTest != null)
            {
                if (blockTest.gameObject.tag == tag && canDealDamage)
                {
                    targetStats.TakeDamage(agentStats.damage.GetValue());
                    canDealDamage = false;
                    if (fxManager != null)
                    {
                        fxManager.Shake();
                    }
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(handTransform.position + handOffset, radius);
    }
}
